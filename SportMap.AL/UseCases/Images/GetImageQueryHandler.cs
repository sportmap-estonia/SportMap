using Microsoft.Extensions.Logging;
using SportMap.AL.Abstractions.Services;
using SportMap.AL.Abstractions.UseCases;
using SportMap.AL.Constants;
using SportMap.DAL.Abstractions;

namespace SportMap.AL.UseCases.Images
{
    public record GetImageQuery(Guid Id) : IQuery<ImageServeResult>;

    public record ImageServeResult(byte[] Content, string ContentType);

    public class GetImageQueryHandler(
        IUnitOfWork unitOfWork,
        IImageStorageService storageService,
        ICacheService cache,
        ILogger<GetImageQueryHandler> logger
    ) : IQueryHandler<GetImageQuery, ImageServeResult>
    {
        public async Task<Result<ImageServeResult>> Handle(GetImageQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("{class}.{method}: Serving image {id}",
                nameof(GetImageQueryHandler), nameof(Handle), query.Id);

            try
            {
                var cacheKey = $"image:{query.Id}";

                // 1. Cache hit
                if (cache.ExistsAsync(cacheKey, cancellationToken))
                {
                    var cachedBytes = await cache.GetAsync<byte[]>(cacheKey, cancellationToken);
                    if (cachedBytes is not null)
                        return Result<ImageServeResult>.WithData(
                            new ImageServeResult(cachedBytes, DetectContentType(cachedBytes)));
                }

                // 2. DB lookup + soft-delete check (no file I/O until after this)
                var image = await unitOfWork.ImageRepository.GetByIdAsync(query.Id, cancellationToken);
                if (image is null || image.RemovedAt != null)
                    return Result<ImageServeResult>.WithError(
                        string.Format(ResultConstants.NotFound, query.Id));

                // 3. Read file bytes from storage
                byte[] bytes;
                try
                {
                    var fileStream = await storageService.ReadStreamAsync(image.Path);
                    using var ms = new MemoryStream();
                    await using (fileStream)
                        await fileStream.CopyToAsync(ms, cancellationToken);
                    bytes = ms.ToArray();
                }
                catch (ImageStorageException)
                {
                    return Result<ImageServeResult>.WithError(ResultConstants.StorageUnavailable);
                }

                // 4. Cache and return
                await cache.SetAsync(cacheKey, bytes, TimeSpan.FromDays(1), cancellationToken);
                return Result<ImageServeResult>.WithData(
                    new ImageServeResult(bytes, DetectContentType(bytes)));
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                logger.LogWarning("{class}.{method}: Operation canceled for image {id}",
                    nameof(GetImageQueryHandler), nameof(Handle), query.Id);
                return Result<ImageServeResult>.WithError("Operation was canceled.");
            }
            catch (Exception e)
            {
                logger.LogError(e, "{class}.{method}: Unhandled exception for image {id}: {msg}",
                    nameof(GetImageQueryHandler), nameof(Handle), query.Id, e.Message);
                return Result<ImageServeResult>.WithError(e.Message);
            }
        }

        private static string DetectContentType(byte[] bytes)
        {
            if (bytes.Length >= 2 && bytes[0] == 0xFF && bytes[1] == 0xD8)
                return "image/jpeg";
            if (bytes.Length >= 4 && bytes[0] == 0x89 && bytes[1] == 0x50
                && bytes[2] == 0x4E && bytes[3] == 0x47)
                return "image/png";
            return "application/octet-stream";
        }
    }
}
