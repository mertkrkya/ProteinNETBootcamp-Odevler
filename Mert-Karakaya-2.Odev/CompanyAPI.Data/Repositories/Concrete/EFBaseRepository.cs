using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyAPI.Core;
using CompanyAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Data.Repositories
{
    public class EFBaseRepository<T> : IBaseRepository<T> where T : class //T'nin ne olduğu her zaman belirtilmeli
    {
        protected readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;

        public EFBaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<T>(); //Context'ten set edilir.
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _dbSet.FindAsync(id);
            if (result != null)
                _appDbContext.Entry(result).State = EntityState.Detached;
            return result;
        }

        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}