using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyAPI.Data.DTO
{
    public class FolderDto
    {
        public int FolderId { get; set; }
        public int EmpId { get; set; }
        public string AccessType { get; set; }
    }
}
