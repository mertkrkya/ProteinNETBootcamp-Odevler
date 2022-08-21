using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompanyAPI.Core;
using CompanyAPI.Core.Entities;

namespace CompanyAPI.Service.Services
{
    public class BaseService<Dto,T> : IBaseService<Dto,T> where T : class
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IUnitofWork _unitofWork;
        protected readonly IMapper _mapper;
        public BaseService(IBaseRepository<T> repository, IUnitofWork unitofWork, IMapper mapper) : base()
        {
            _repository = repository;
            _unitofWork = unitofWork;
            _mapper = mapper;
        }
        public virtual async Task<ResponseEntity> GetAllAsync()
        {
            try
            {
                var allRecord = await _repository.GetAllAsync();
                var mappedResult = _mapper.Map<IEnumerable<T>, IEnumerable<Dto>>(allRecord);
                return new ResponseEntity(mappedResult);
            }
            catch (Exception e)
            {
                return new ResponseEntity("Get All Error");
            }
        }

        public virtual async Task<ResponseEntity> GetByIdAsync(int id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);
                var mappedResult = _mapper.Map<T, Dto>(result);
                return new ResponseEntity(mappedResult);
            }
            catch (Exception e)
            {
                return new ResponseEntity("No Data with ID: "+id);
            }
        }

        public virtual async Task<ResponseEntity> InsertAsync(Dto entity)
        {
            try
            {
                var tempEntity = _mapper.Map<Dto, T>(entity);
                var result = _repository.InsertAsync(tempEntity);
                await _unitofWork.CommitAsync();
                return new ResponseEntity(entity);
            }
            catch (Exception e)
            {
                return new ResponseEntity("Save Error");
            }
        }

        public virtual async Task<ResponseEntity> UpdateAsync(int id, Dto entity)
        {
            try
            {
                var unUpdatedEntity = await _repository.GetByIdAsync(id);
                if (unUpdatedEntity == null)
                {
                    return new ResponseEntity("No Data");
                }
                var tempEntity = _mapper.Map<Dto, T>(entity);
                _repository.Update(tempEntity);
                await _unitofWork.CommitAsync();
                return new ResponseEntity(entity);
            }
            catch (Exception e)
            {
                return new ResponseEntity("Update Error");
            }
        }

        public virtual async Task<ResponseEntity> DeleteAsync(int id)
        {
            try
            {
                var deleteEntity = await _repository.GetByIdAsync(id);
                if (deleteEntity == null)
                {
                    return new ResponseEntity("No Data");
                }
                _repository.Delete(deleteEntity);
                _unitofWork.Commit();
                return new ResponseEntity(deleteEntity);
            }
            catch (Exception e)
            {
                return new ResponseEntity("Delete Error");
            }
        }
    }
}
