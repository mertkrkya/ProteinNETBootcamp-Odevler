using System;
using System.Collections.Generic;
using System.Text;
using CompanyAPI.Data.Context;
using CompanyAPI.Data.Model;

namespace CompanyAPI.Data.Repositories
{
    public class FolderRepository : EFBaseRepository<Folder>, IFolderRepository
    {
        public FolderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
