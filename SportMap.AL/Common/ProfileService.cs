using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportMap.AL.Abstractions.Dtos;
using SportMap.AL.Abstractions.Services;
using SportMap.DAL.Abstractions;

namespace SportMap.AL.Common
{
    public class ProfileService(
        IUserRepository userRepository,
        ILogger<ProfileService> logger
    ) : IProfileService
    {
        public async Task<UserProfileDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("{class}.{method}: id={Id}",
                nameof(ProfileService), nameof(GetByIdAsync), id);

            var user = await userRepository.GetByIdWithProfileAsync(id, cancellationToken);

            if (user is null)
                return null;

            return new UserProfileDto
            {
                Id        = user.Id,
                UserName  = user.UserName,
                Email     = user.Email,
                FirstName = user.FirstName,
                LastName  = user.LastName,
                RoleName  = user.UserRole?.Name,
                Birthdate = IsBirthdatePublic(user) ? user.Birthdate : null,
            };
        }

        private static bool IsBirthdatePublic(User user) =>
            user.Personalization?.BirthdatePrivacyType?.Name
                .Equals("public", StringComparison.OrdinalIgnoreCase) == true;
    }
}