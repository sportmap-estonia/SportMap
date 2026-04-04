using SportMap.AL.Abstractions.Dtos;
using SportMap.AL.Abstractions.UseCases;
using SportMap.AL.Constants;
using SportMap.DAL.Abstractions;

namespace SportMap.AL.UseCases.Images
{
    public record GetProfilePictureByUsernameQuery(string Username) : IQuery<UserProfilePictureDto>;

    public class GetProfilePictureByUsernameQueryHandler(IUnitOfWork unitOfWork)
        : IQueryHandler<GetProfilePictureByUsernameQuery, UserProfilePictureDto>
    {
        public async Task<Result<UserProfilePictureDto>> Handle(
            GetProfilePictureByUsernameQuery query,
            CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository.GetByUserNameAsync(
                query.Username, cancellationToken);

            if (user is null)
                return Result<UserProfilePictureDto>.WithError(
                    string.Format(ResultConstants.NotFound, query.Username));

            return Result<UserProfilePictureDto>.WithData(
                new UserProfilePictureDto { Id = user.Id, ProfilePictureId = user.ProfilePictureId });
        }
    }
}
