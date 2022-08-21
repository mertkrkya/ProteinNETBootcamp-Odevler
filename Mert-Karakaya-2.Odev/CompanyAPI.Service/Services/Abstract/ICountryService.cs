using System;
using System.Collections.Generic;
using System.Text;
using CompanyAPI.Core;
using CompanyAPI.Data.Model;
using CompanyAPI.Data.DTO;

namespace CompanyAPI.Service.Services
{
    public interface ICountryService : IBaseService<CountryDto,Country>
    {
    }
}
