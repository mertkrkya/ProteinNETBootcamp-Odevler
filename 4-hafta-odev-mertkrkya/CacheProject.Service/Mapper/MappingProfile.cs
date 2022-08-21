using AutoMapper;
using CacheProject.Core.Dto;
using CacheProject.Core.Models;

namespace CacheProject.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
        }
    }
}
