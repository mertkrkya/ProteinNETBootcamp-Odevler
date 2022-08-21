using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JWTProject.Core.Repositories;
using JWTProject.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace JWTProject.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _appDbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<TEntity>(); //Context'ten set edilir.
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var result = await _dbSet.FindAsync(id);
            if (result != null)
                _appDbContext.Entry(result).State = EntityState.Detached;
            return result;
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
