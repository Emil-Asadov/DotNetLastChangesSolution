using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MinimalAPIRealProject.Service
{
    public sealed class JwtProvider
    {
        public string CreateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my secret key my secret key my secret key my secret key my secret key"));
            JwtSecurityToken jwtSecurityToken = new
                (
                issuer: "Emil Asadov",
                audience: "Emil Asadov",
                claims: null,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(10),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
                );

            JwtSecurityTokenHandler handler = new();
            var token = handler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
