using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SportMap.AL.Abstractions.Dtos;
using SportMap.AL.Abstractions.Services;
using SportMap.DAL.Common;
using SportMap.DAL.Models;

namespace SportMap.AL.Common
{
    public class AuthService(
        IUserRepository userRepository,
        IConfiguration configuration,
        ILogger<AuthService> logger,
        HttpClient httpClient) : IAuthService
    {
        public async Task<AuthResponseDto> GoogleCallbackAsync(string code, string codeVerifier, CancellationToken cancellationToken = default)
        {
            var payload = await ExchangeCodeAsync(code, codeVerifier, cancellationToken);
            var user = await userRepository.GetByGoogleIdAsync(payload.Subject, cancellationToken);
            if (user is null)
            {
                user = new User
                {
                    Id          = Guid.NewGuid(),
                    GoogleId    = payload.Subject,
                    Email       = payload.Email,
                    UserName    = payload.Email.Split('@')[0], // for now use mail initials for username
                    FirstName   = payload.GivenName ?? payload.Name.Split(' ').FirstOrDefault() ?? string.Empty,
                    LastName    = payload.FamilyName,
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
                    Id = user.Id,
                    Email = user.Email,
                    Name = $"{user.FirstName} {user.LastName}".Trim()
                }
            };
        }

        private async Task<GoogleJsonWebSignature.Payload> ExchangeCodeAsync(string code, string codeVerifier, CancellationToken cancellationToken)
        {
            var clientId = configuration["Google:ClientId"] ?? throw new InvalidOperationException("Google:ClientId is missing");
            var clientSecret = configuration["Google:ClientSecret"] ?? throw new InvalidOperationException("Google:ClientSecret missing");
            var redirectUri = configuration["Google:RedirectUri"] ?? throw new InvalidOperationException("Google:RedirectUri missing");

            var tokenResponse = await httpClient.PostAsync(
                "https://oauth2.googleapis.com/token",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["code"]            = code,
                    ["client_id"]       = clientId,
                    ["client_secret"]   = clientSecret,
                    ["redirect_uri"]    = redirectUri,
                    ["grant_type"]      = "authorization_code",
                    ["code_verifier"]   = codeVerifier
                }), cancellationToken);

            tokenResponse.EnsureSuccessStatusCode();

            var jsonData = await tokenResponse.Content.ReadAsStringAsync(cancellationToken);
            var idToken = JsonDocument.Parse(jsonData).RootElement.GetProperty("id_token").GetString()
                ?? throw new InvalidOperationException("Invalid token response from Google");
            return await GoogleJsonWebSignature.ValidateAsync(idToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = [clientId]
                });
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
                new Claim(ClaimTypes.Role, "member")
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