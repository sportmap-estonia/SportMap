using DomainLayer.Entities;

namespace SportMap.AL.Abstractions.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}