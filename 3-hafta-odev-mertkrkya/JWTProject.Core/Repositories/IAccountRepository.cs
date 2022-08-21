using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTProject.Core.Entities;
using JWTProject.Core.Models;

namespace JWTProject.Core.Repositories
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<Account> ValidateLoginAsync(LoginRequest loginRequest);
    }
}
