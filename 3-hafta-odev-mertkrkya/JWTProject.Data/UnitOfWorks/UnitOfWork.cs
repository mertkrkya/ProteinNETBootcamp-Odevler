using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JWTProject.Core.UnitofWork;
using JWTProject.Data.Context;

namespace JWTProject.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitofWork
    {
        private readonly AppDbContext dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
