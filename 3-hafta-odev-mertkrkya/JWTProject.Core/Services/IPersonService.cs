using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JWTProject.Core.Dto;
using JWTProject.Core.Entities;
using JWTProject.Core.Models;
using JWTProject.Core.Services;

namespace JWTProject.Core.Services
{
    public interface IPersonService : IBaseService<PersonDto,Person>
    {
        Task<ResponseEntity> GetPeopleByAccountIdAsync(int accountId);
    }
}
