using Domain.Entities;
using Domain.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application.HelpClasses
{
    public sealed class JWTGenerator(IConfiguration configuration)
    {
        public string Create(User user) {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = user.GetClaims(),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("JWT:ExpireMinutes")),
                SigningCredentials = signingCredentials
            };
            
            return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
        }
    }
}
