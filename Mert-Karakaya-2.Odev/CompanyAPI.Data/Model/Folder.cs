using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyAPI.Data.Model
{
    public class Folder
    {
        public int FolderId { get; set; }
        public int EmpId { get; set; }
        public string AccessType { get; set; }
        public Employee Employee { get; set; }
    }
}
