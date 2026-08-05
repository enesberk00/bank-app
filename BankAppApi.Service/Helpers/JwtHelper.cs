using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankAppApi.Repository.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BankAppApi.Service.Helpers
{
    public static class JwtHelper
    {
        public static string GenerateJwtToken(User user, IConfiguration configuration)
        {
            // 1. Get the key from appsettings.json to encrypt the token
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!));

            // 2. Create a signature using the key
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Define the claims to be included in the token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.Id.ToString())
            };

            // 4. Define the token (Who is issuing, who is receiving, when it expires)
            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(configuration["JwtSettings:ExpireMinutes"]!)),
                signingCredentials: credentials
            );

            // 5. Return the token in string (text) format
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

