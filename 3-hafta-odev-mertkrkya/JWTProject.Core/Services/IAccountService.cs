using System;
using System.Collections.Generic;
using System.Text;
using JWTProject.Core.Dto;
using JWTProject.Core.Models;
using JWTProject.Core.Services;

namespace JWTProject.Core.Services
{
    public interface IAccountService : IBaseService<AccountDto,Account>
    {
    }
}
