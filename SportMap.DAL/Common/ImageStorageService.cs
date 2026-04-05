using Microsoft.Extensions.Logging;
using SportMap.AL.Abstractions.Services;

namespace SportMap.DAL.Common
{
    public class ImageStorageService(ILogger<ImageStorageService> logger) : IImageStorageService
    {
        public async Task WriteAsync(Stream content, string path)
        {
            try
            {
                var directory = System.IO.Path.GetDirectoryName(path)!;
                Directory.CreateDirectory(directory);
                await using var fileStream = File.Create(path);
                await content.CopyToAsync(fileStream);
            }
            catch (Exception e) when (e is IOException or DirectoryNotFoundException or UnauthorizedAccessException)
            {
                logger.LogError(e, "{class}.{method}: Storage I/O failure writing {path}",
                    nameof(ImageStorageService), nameof(WriteAsync), path);
                throw new ImageStorageException($"Failed to write file at {path}.", e);
            }
        }

        public async Task<Stream> ReadStreamAsync(string path)
        {
            try
            {
                return await Task.FromResult(File.OpenRead(path));
            }
            catch (Exception e) when (e is IOException or DirectoryNotFoundException or UnauthorizedAccessException)
            {
                logger.LogError(e, "{class}.{method}: Storage I/O failure reading {path}",
                    nameof(ImageStorageService), nameof(ReadStreamAsync), path);
                throw new ImageStorageException($"Failed to read file at {path}.", e);
            }
        }

        public Task DeleteAsync(string path)
        {
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
                return Task.CompletedTask;
            }
            catch (Exception e) when (e is IOException or DirectoryNotFoundException or UnauthorizedAccessException)
            {
                logger.LogError(e, "{class}.{method}: Storage I/O failure deleting {path}",
                    nameof(ImageStorageService), nameof(DeleteAsync), path);
                throw new ImageStorageException($"Failed to delete file at {path}.", e);
            }
        }
    }
}
