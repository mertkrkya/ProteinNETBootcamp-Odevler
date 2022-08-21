using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CompanyAPI.Data.DTO;
using CompanyAPI.Data.Model;

namespace CompanyAPI.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Folder, FolderDto>().ReverseMap();
        }
    }
}
