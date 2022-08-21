using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyAPI.Data.Model
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public int DeptId { get; set; }
        public Department Department { get; set; }
        public ICollection<Folder> Folders { get; set; }
    }
}
