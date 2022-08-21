using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheProject.Core.Entities;

namespace CacheProject.Core.Services
{
    public interface IBaseService<Dto, T>
    {
        Task<ResponseEntity> GetAllAsync();
        Task<ResponseEntity> GetByIdAsync(int id);
        Task<ResponseEntity> InsertAsync(Dto entity);
        Task<ResponseEntity> UpdateAsync(int id, Dto entity);
        Task<ResponseEntity> DeleteAsync(int id);
    }
}
