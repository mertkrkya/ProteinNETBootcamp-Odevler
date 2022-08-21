using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTProject.Core.Models;

namespace JWTProject.Core.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<IEnumerable<Person>> GetPeopleByAccountIdAsync(int AccountId);
    }
}
