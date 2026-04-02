using SportMap.AL.Abstractions.Dtos;

namespace SportMap.AL.Abstractions.Services
{
    public interface IProfileService
    {
        Task<UserProfileDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}