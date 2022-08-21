using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyAPI.Data.Model
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Continent { get; set; }
        public string Currency { get; set; }
        public ICollection<Department> Departments { get; set; }

    }
}
