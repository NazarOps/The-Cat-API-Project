using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cat_API_Project.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Cat_API_Project.Services
{
    public class JwtService : IJwtService
    {
        public readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(int accountId, string username, string email)
        {
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidOperationException("JWT key is missing.");
            }

            // create claims info inside token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, accountId.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email)
            };

            // signing key with security algorithm
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //sets expiry time to 1 hour
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            //return token as string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
