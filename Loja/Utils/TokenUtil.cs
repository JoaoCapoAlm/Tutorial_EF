using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Loja.Utils
{
    public static class TokenUtil
    {
        public static string GenerateToken(string? userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("dfhviocsjserkvknkjsdajvbejnvjfjsdf")),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Claims = new Dictionary<string, object>
                {
                    { ClaimTypes.NameIdentifier, userId }
                }
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
