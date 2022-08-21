using System;
using System.Collections.Generic;
using System.Text;
using CompanyAPI.Core;
using CompanyAPI.Data.DTO;
using CompanyAPI.Data.Model;

namespace CompanyAPI.Service.Services
{
    public interface IEmployeeService : IBaseService<EmployeeDto,Employee>
    {
    }
}
