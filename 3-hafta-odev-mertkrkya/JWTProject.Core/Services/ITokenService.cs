using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTProject.Core.Dto;
using JWTProject.Core.Models;

namespace JWTProject.Core.Services
{
    public interface ITokenService
    {
        JwtTokenDto CreateToken(Account account);
    }
}
