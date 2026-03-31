using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DomainLayer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SportMap.AL.Abstractions.Services;

namespace SportMap.AL.Common
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        public string GenerateAccessToken(User user)
        {
            var secretKey = configuration["Jwt:SecretKey"]
                ?? throw new InvalidOperationException("Jwt:SecretKey missing");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,      user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,    user.Email),
                new Claim(JwtRegisteredClaimNames.Name,     user.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti,      Guid.NewGuid().ToString()),
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
        
        public string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}