using SportMap.AL.Abstractions.Dtos;

namespace SportMap.AL.Abstractions.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> ProcessGoogleLoginAsync(GoogleUserInfoDto googleUserInfo, CancellationToken ct = default);
    }
}