using CompanyAPI.Data.DTO;
using CompanyAPI.Service.Services;
using CompanyAPI.Service.Services.Abstract;

namespace CompanyAPI.Helpers
{
    public static class Validator
    {
        public static string DepartmentValidator(DepartmentDto entity, ICountryService ctx)
        {
            if (string.IsNullOrWhiteSpace(entity.DeptName)) return "Departman Adı boş bırakılamaz.";

            if (entity.CountryId == 0)
            {
                return "Country ID boş bırakılamaz.";
            }

            var countryResult = ctx.GetByIdAsync(entity.CountryId).Result;
            if (countryResult != null)
                if (countryResult.data == null)
                    return "Bu Country ID numarası mevcut değildir. Country ID:" + entity.CountryId;
            return "";
        }

        public static string CountryValidator(CountryDto entity)
        {
            if (string.IsNullOrWhiteSpace(entity.CountryName)) return "Country Name boş bırakılamaz.";

            if (string.IsNullOrWhiteSpace(entity.Continent)) return "Country Continent boş bırakılamaz.";

            if (string.IsNullOrWhiteSpace(entity.Currency))
            {
                return "Currency boş bırakılamaz.";
            }

            if (entity.Currency.Length > 3) return "Currency uzunluğu 3'ten büyük olamaz..";
            return "";
        }

        public static string EmployeeValidator(EmployeeDto entity, IDepartmentService ctx)
        {
            if (string.IsNullOrWhiteSpace(entity.EmpName)) return "Employee Name boş bırakılamaz.";

            if (entity.DeptId == 0)
            {
                return "Dept ID boş bırakılamaz.";
            }

            var deptResult = ctx.GetByIdAsync(entity.DeptId).Result;
            if (deptResult != null)
                if (deptResult.data == null)
                    return "Bu Dept ID numarası mevcut değildir. Dept ID:" + entity.DeptId;
            return "";
        }

        public static string FolderValidator(FolderDto entity, IEmployeeService ctx)
        {
            if (string.IsNullOrWhiteSpace(entity.AccessType)) return "AccessType boş bırakılamaz.";

            if (entity.AccessType.Length > 5) return "AccessType uzunluğu 5'ten büyük olamaz.";

            if (entity.EmpId == 0)
            {
                return "Emp ID boş bırakılamaz.";
            }

            var empResult = ctx.GetByIdAsync(entity.EmpId).Result;
            if (empResult != null)
                if (empResult.data == null)
                    return "Bu Emp ID numarası mevcut değildir. Emp ID:" + entity.EmpId;
            return "";
        }
    }
}