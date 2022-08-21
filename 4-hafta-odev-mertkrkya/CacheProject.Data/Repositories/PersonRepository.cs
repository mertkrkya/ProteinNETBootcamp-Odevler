using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheProject.Core.Models;
using CacheProject.Core.Repositories;
using CacheProject.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CacheProject.Data.Repositories
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

    }
}
