using KarmaBook.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KarmaBook.Extensions
{
    public class TokenService
    {
        private readonly IOptions<JWTSettings> _jwtSettings;
        public TokenService(IConfiguration configuration, IOptions<JWTSettings> jWTSettings)
        {
            _jwtSettings = jWTSettings;
        }
        public string Create(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var privateKey = Encoding.UTF8.GetBytes(_jwtSettings.Value.Key);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.Value.ExpiryMinutes),
                Subject = GenerateClaims(user)
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
            
        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim("id",user.UserId.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.FirstName));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.FirstName));

            if ((user.Roles & Role.Admin)== Role.Admin)
            {
                ci.AddClaim(new Claim(ClaimTypes.Role, Role.Admin.ToString()));
            }
            if ((user.Roles & Role.User) == Role.User)
            {
                ci.AddClaim(new Claim(ClaimTypes.Role, Role.User.ToString()));
            }
            if ((user.Roles & Role.Seller) == Role.Seller)
            {
                ci.AddClaim(new Claim(ClaimTypes.Role, Role.Seller.ToString()));
            }

            return ci;
        }
    }
}
