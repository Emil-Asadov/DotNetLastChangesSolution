using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MinimalAPIRealProject.Models.DTO;
using MinimalAPIRealProject.Models.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalAPIRealProject.Service
{
    public sealed class JwtProvider(IOptions<JwtSettings> jwtSettings)
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        public TokenResponse CreateToken(UserDto userDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var now = DateTimeOffset.UtcNow;
            var claimArray = new[]
            {
                //new Claim(ClaimTypes.NameIdentifier,apiUser.Name!),
                //new Claim(ClaimTypes.Role,apiUser.Role!),
                new Claim("author","emil.asadov"),
                new Claim("role",userDto.Role),
                new Claim(JwtRegisteredClaimNames.Sub,userDto.Name),
                new Claim(JwtRegisteredClaimNames.Iat,/*((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds().ToString()*/now.ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer64)
            };

            JwtSecurityToken jwtSecurityToken = new
                (
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claimArray,
                notBefore: /*DateTime.Now*/now.UtcDateTime,
                expires: /*DateTime.Now.AddMinutes(1)*/now.AddMinutes(1).UtcDateTime,
                signingCredentials: credentials
                );

            JwtSecurityTokenHandler handler = new();
            var tokenHandler = handler.WriteToken(jwtSecurityToken);

            TokenResponse token = new(AccessToken: tokenHandler);

            return token;
        }
    }
}
