using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyAPI.Data.Model
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DeptName { get; set; }
        public int CountryId { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public Country Country { get; set; }
    }
}
