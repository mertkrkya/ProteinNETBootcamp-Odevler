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

namespace CompanyAPI.Service.Services
{
    public class EmployeeService : BaseService<EmployeeDto,Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IUnitofWork _unitofWork;
        public EmployeeService(IEmployeeRepository repository, IUnitofWork unitofWork, IMapper mapper) : base(repository, unitofWork, mapper)
        {
            employeeRepository = repository;
            _unitofWork = unitofWork;
        }
        public override async Task<ResponseEntity> UpdateAsync(int id, EmployeeDto entity)
        {
            try
            {
                if (id != entity.EmpId)
                {
                    return new ResponseEntity("Not Equal ID with Employee ID. EmpID: " + entity.EmpId + " ID:" + id);
                }
                var unUpdatedEntity = await employeeRepository.GetByIdAsync(id);
                if (unUpdatedEntity == null)
                {
                    return new ResponseEntity("No Data");
                }
                var tempEntity = _mapper.Map<EmployeeDto, Employee>(entity);
                employeeRepository.Update(tempEntity);
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
