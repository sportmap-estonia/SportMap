using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SportMap.AL.Abstractions.Dtos;
using SportMap.AL.Abstractions.Services;
using SportMap.AL.Abstractions.UseCases;
using SportMap.DAL.Abstractions;

namespace SportMap.AL.UseCases.Images
{
    public record UploadProfilePictureCommand(
        Stream FileStream,
        string FileName,
        long FileSize,
        Guid UploaderId
    ) : ICommand<UploadImageResponseDto>;

    public class UploadProfilePictureCommandHandler(
        IUnitOfWork unitOfWork,
        IImageStorageService storageService,
        IOptions<ImageStorageOptions> options,
        ICacheService cache,
        ILogger<UploadProfilePictureCommandHandler> logger
    ) : ICommandHandler<UploadProfilePictureCommand, UploadImageResponseDto>
    {
        public Task<Result<UploadImageResponseDto>> Handle(
            UploadProfilePictureCommand command,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException("");
        }
    }
}
