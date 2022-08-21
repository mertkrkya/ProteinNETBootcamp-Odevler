using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTProject.Core.Entities;
using JWTProject.Core.Models;
using JWTProject.Core.Repositories;
using JWTProject.Data.Context;

namespace JWTProject.Data.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        protected readonly AppDbContext _appDbContext;
        public AccountRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Account> ValidateLoginAsync(LoginRequest loginRequest)
        {
            var findAccount = _appDbContext.Accounts.FirstOrDefault(r => r.UserName == loginRequest.UserName);
            if (findAccount == null)
                return null;
            if (!string.Equals(loginRequest.Password, findAccount.Password))
                return null;
            return findAccount;
        }
    }
}
