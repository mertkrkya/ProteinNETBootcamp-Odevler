using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompanyAPI.Core;
using CompanyAPI.Core.Entities;
using CompanyAPI.Data.DTO;
using CompanyAPI.Data.Model;
using CompanyAPI.Data.Repositories;
using CompanyAPI.Service.Services.Abstract;

namespace CompanyAPI.Service.Services
{
    public class DepartmentService : BaseService<DepartmentDto, Department>, IDepartmentService
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IUnitofWork _unitofWork;
        public DepartmentService(IDepartmentRepository repository, IUnitofWork unitofWork, IMapper mapper) : base(repository, unitofWork, mapper)
        {
            this.departmentRepository = repository;
            _unitofWork = unitofWork;
        }
        public override async Task<ResponseEntity> UpdateAsync(int id, DepartmentDto entity)
        {
            try
            {
                if (id != entity.DepartmentId)
                {
                    return new ResponseEntity("Not Equal ID with DepartmentId. CountryID: " + entity.DepartmentId + " ID:" + id);
                }
                var unUpdatedEntity = await departmentRepository.GetByIdAsync(id);
                if (unUpdatedEntity == null)
                {
                    return new ResponseEntity("No Data");
                }
                var tempEntity = _mapper.Map<DepartmentDto, Department>(entity);
                departmentRepository.Update(tempEntity);
                await _unitofWork.CommitAsync();
                return new ResponseEntity(entity);
            }
            catch (Exception e)
            {
                return new ResponseEntity("Update Error");
            }
        }
    }
}
