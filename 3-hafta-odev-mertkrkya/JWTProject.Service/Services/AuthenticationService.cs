using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTProject.Core.Entities;
using JWTProject.Core.Models;
using JWTProject.Core.Repositories;
using JWTProject.Core.Services;
using JWTProject.Core.UnitofWork;
using Microsoft.EntityFrameworkCore;

namespace JWTProject.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitofWork _unitofWork;
        private readonly IBaseRepository<AccountRefreshToken> _refreshTokenRepository;
        public AuthenticationService(ITokenService tokenService, IUnitofWork unitofWork, IBaseRepository<AccountRefreshToken> refreshTokenRepository,
            IAccountRepository accountRepository)
        {
            _tokenService = tokenService;
            _unitofWork = unitofWork;
            _refreshTokenRepository = refreshTokenRepository;
            _accountRepository = accountRepository;
        }
        public async Task<ResponseEntity> CreateTokenAsync(LoginRequest login)
        {
            if (login == null)
                return new ResponseEntity("Giriş bilgileri boş olamaz.");
            var account = await _accountRepository.ValidateLoginAsync(login);
            if (account == null)
                return new ResponseEntity("Geçersiz kullanıcı bilgileri");
            var token = _tokenService.CreateToken(account);
            var accountRefreshToken = await _refreshTokenRepository.Find(r => r.AccountId == account.Id).FirstOrDefaultAsync();
            if (accountRefreshToken == null)
            {
                await _refreshTokenRepository.InsertAsync((new AccountRefreshToken()
                {
                    AccountId = account.Id,
                    Code = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration
                }));
            }
            else
            {
                accountRefreshToken.Code = token.RefreshToken;
                accountRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitofWork.CommitAsync();
            return new ResponseEntity(token);
        }

        public async Task<ResponseEntity> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var accountRefreshToken = await _refreshTokenRepository.Find(r => r.Code == refreshToken).FirstOrDefaultAsync();
            if (accountRefreshToken == null)
                return new ResponseEntity("Refresh Token verisi bulunamadı.");
            var existAccount = await _accountRepository.Find(r => r.Id == accountRefreshToken.AccountId).FirstOrDefaultAsync();
            if (existAccount == null)
                return new ResponseEntity("Geçersiz Accound ID");
            var token = _tokenService.CreateToken(existAccount);

            accountRefreshToken.Code = token.RefreshToken;
            accountRefreshToken.Expiration = token.RefreshTokenExpiration;
            await _unitofWork.CommitAsync();
            return new ResponseEntity(token);
        }
    }
}
