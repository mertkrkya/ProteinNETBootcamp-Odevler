using AutoMapper;
using JWTProject.Core.Dto;
using JWTProject.Core.Models;

namespace JWTProject.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
        }
    }
}
