using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CompanyAPI.Data.Context;
using CompanyAPI.Data.Model;
using Dapper;

namespace CompanyAPI.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DapperDbContext dapperDbContext;

        public DepartmentRepository(DapperDbContext dapperDbContext)
        {
            this.dapperDbContext = dapperDbContext;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var sql = "SELECT * FROM department";
            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Department>(sql);
                return result;
            }
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM department WHERE DepartmentId = @id";
            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Department>(query, new { id });
                return result;
            }
        }


        public async Task InsertAsync(Department entity)
        {
            var query = "INSERT INTO department (\"deptname\", \"countryid\") " +
                        "VALUES (@deptname, @countryid)";

            var parameters = new DynamicParameters();
            parameters.Add("deptname", entity.DeptName, DbType.String);
            parameters.Add("countryid", entity.CountryId, DbType.Int32);

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public void Update(Department entity)
        {
            var query =
                "UPDATE department SET \"deptname\" = @deptname , \"countryid\" = @countryid WHERE \"departmentid\" = @departmentid";


            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                connection.Execute(query, new { entity.DeptName, entity.CountryId, entity.DepartmentId });
            }
        }

        public void Delete(Department entity)
        {
            var query = "DELETE FROM department WHERE \"departmentid\" = @departmentid";
            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                connection.Execute(query, new { entity.DepartmentId });
            }
        }
    }
}