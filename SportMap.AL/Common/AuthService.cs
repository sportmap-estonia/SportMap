using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DomainLayer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SportMap.AL.Abstractions.Dtos;
using SportMap.AL.Abstractions.Services;
using SportMap.DAL.Common;

namespace SportMap.AL.Common
{
    public class AuthService(
        IUserRepository userRepository,
        IConfiguration configuration
    ) : IAuthService
    {
        public async Task<AuthResponseDto> AuthenticateWithGoogleAsync(GoogleUserInfoDto googleUserInfo, CancellationToken cancellationToken = default)
        {
            var user = await userRepository.GetByGoogleIdAsync(googleUserInfo.GoogleId, cancellationToken);
            if (user is null)
            {
                user = new User
                {
                    Id          = Guid.NewGuid(),
                    GoogleId    = googleUserInfo.GoogleId,
                    Email       = googleUserInfo.Email,
                    UserName    = googleUserInfo.Email.Split('@')[0], // for now use mail initials for username
                    FirstName   = googleUserInfo.GivenName ?? string.Empty,
                    LastName    = googleUserInfo.FamilyName,
                    CreatedAt   = DateTime.UtcNow
                };
                await userRepository.AddAsync(user, cancellationToken);
            }

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();
            var expiresAt = DateTimeOffset.UtcNow.AddMinutes(15).ToUnixTimeMilliseconds();

            return new AuthResponseDto
            {
                AccessToken     = accessToken,
                RefreshToken    = refreshToken,
                ExpiresAt       = expiresAt,
                User            = new UserDto
                {
                    Id      = user.Id,
                    Email   = user.Email,
                    Name    = $"{user.FirstName} {user.LastName}".Trim()
                }
            };
        }

        private string GenerateAccessToken(User user)
        {
            var secretKey = configuration["Jwt:SecretKey"]
                ?? throw new InvalidOperationException("Jwt:SecretKey missing");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name,  user.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "member") // TODO: add roles to table
            };

            var token = new JwtSecurityToken(
                issuer:             configuration["Jwt:Issuer"],
                audience:           configuration["Jwt:Audience"],
                claims:             claims,
                expires:            DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}