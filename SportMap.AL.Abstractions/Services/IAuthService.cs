using SportMap.AL.Abstractions.Dtos;

namespace SportMap.AL.Abstractions.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> AuthenticateWithGoogleAsync(GoogleUserInfoDto googleUserInfo, CancellationToken ct = default);
    }
}