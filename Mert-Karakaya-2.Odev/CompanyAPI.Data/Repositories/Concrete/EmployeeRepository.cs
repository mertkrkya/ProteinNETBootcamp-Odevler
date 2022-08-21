using System;
using System.Collections.Generic;
using System.Text;
using CompanyAPI.Data.Context;
using CompanyAPI.Data.Model;

namespace CompanyAPI.Data.Repositories
{
    public class EmployeeRepository : EFBaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
