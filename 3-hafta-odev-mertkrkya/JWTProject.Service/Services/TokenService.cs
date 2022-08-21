using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JWTProject.Core.Configurations;
using JWTProject.Core.Dto;
using JWTProject.Core.Models;
using JWTProject.Core.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWTProject.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfig _jwtConfig;

        public TokenService(IOptions<JwtConfig> options)
        {
            _jwtConfig = options.Value;
        }

        public JwtTokenDto CreateToken(Account account)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_jwtConfig.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_jwtConfig.RefreshTokenExpiration);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature); //İmzayı oluşturuyoruz.
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(account),
                signingCredentials: signingCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwtSecurityToken);
            JwtTokenDto jwtToken = new JwtTokenDto();
            jwtToken.AccessToken = token;
            jwtToken.AccessTokenExpiration = accessTokenExpiration;
            jwtToken.RefreshToken = CreateRefreshToken();
            jwtToken.RefreshTokenExpiration = refreshTokenExpiration;
            return jwtToken;
        }
        private string CreateRefreshToken()
        {
            var numberByte = new byte[32];
            var rndNumber = RandomNumberGenerator.Create();
            rndNumber.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> GetClaims(Account account)
        {
            var claimList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Name),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim("AccountId", account.Id.ToString())
            };
            return claimList;
        }
    }
}
