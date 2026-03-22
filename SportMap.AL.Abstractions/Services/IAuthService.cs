using SportMap.AL.Abstractions.Dtos;

namespace SportMap.AL.Abstractions.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> GoogleCallbackAsync(string code, string codeVerifier, CancellationToken cancellationToken = default);
    }
}