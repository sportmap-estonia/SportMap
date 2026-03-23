using SportMap.AL.Abstractions.Dtos;

namespace SportMap.AL.Abstractions.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> GoogleCallbackAsync(GoogleUserInfoDto googleUserInfo, CancellationToken ct = default);
    }
}