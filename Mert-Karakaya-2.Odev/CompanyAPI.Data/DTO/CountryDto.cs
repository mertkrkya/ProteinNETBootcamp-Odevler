using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyAPI.Data.DTO
{
    public class CountryDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Continent { get; set; }
        public string Currency { get; set; }

    }
}
