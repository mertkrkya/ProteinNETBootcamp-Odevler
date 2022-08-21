using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CompanyAPI.Core
{
    public interface IBaseRepository<T> where T : class
    {
        //T benim için bir Class'tır.
        //IQueryable daha performanslıdır. ToList çekildikten sonra DB'ye gidip istek atar.
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T entity);
        void Update(T entity); //Uzun işlem olmadığı için senkron da yapılabilir. Direkt stateyi değişiyor.
        void Delete(T entity);
    }
}