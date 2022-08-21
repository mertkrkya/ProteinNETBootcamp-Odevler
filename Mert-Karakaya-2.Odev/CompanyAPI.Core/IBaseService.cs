using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CompanyAPI.Core.Entities;

namespace CompanyAPI.Core
{
    public interface IBaseService<Dto,T>
    {
        Task<ResponseEntity> GetAllAsync();
        Task<ResponseEntity> GetByIdAsync(int id);
        Task<ResponseEntity> InsertAsync(Dto entity);
        Task<ResponseEntity> UpdateAsync(int id, Dto entity);
        Task<ResponseEntity> DeleteAsync(int id);
    }
}