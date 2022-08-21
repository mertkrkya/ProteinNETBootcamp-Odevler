using System.Threading.Tasks;
using JWTProject.Core.Entities;
using JWTProject.Core.Dto;

namespace JWTProject.Core.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseEntity> CreateTokenAsync(LoginRequest login); //TokenDto döneceğim.
        Task<ResponseEntity> CreateTokenByRefreshTokenAsync(string refreshToken); //Refresh Token ile Access Token oluşturma.
    }
}
