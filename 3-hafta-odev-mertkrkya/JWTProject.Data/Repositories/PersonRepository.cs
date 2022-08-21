using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTProject.Core.Models;
using JWTProject.Core.Repositories;
using JWTProject.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace JWTProject.Data.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        protected readonly AppDbContext _appDbContext;
        private readonly DbSet<Person> _dbSet;

        public PersonRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<Person>();
        }

        public async Task<IEnumerable<Person>> GetPeopleByAccountIdAsync(int accountId)
        {
            return await _dbSet.AsNoTracking().Where(r => r.AccountId == accountId).ToListAsync();
        }
    }
}
