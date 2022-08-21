using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompanyAPI.Core;
using CompanyAPI.Core.Entities;
using CompanyAPI.Data.DTO;
using CompanyAPI.Data.Model;
using CompanyAPI.Data.Repositories;

namespace CompanyAPI.Service.Services
{
    public class CountryService : BaseService<CountryDto,Country>, ICountryService
    {
        private readonly ICountryRepository countryRepository;
        private readonly IUnitofWork _unitofWork;
        public CountryService(ICountryRepository repository, IMapper mapper, IUnitofWork unitofWork) : base(repository, unitofWork, mapper)
        {
            this.countryRepository = repository;
            _unitofWork = unitofWork;
        }
        public override async Task<ResponseEntity> UpdateAsync(int id, CountryDto entity)
        {
            try
            {
                if (id != entity.CountryId)
                {
                    return new ResponseEntity("Not Equal ID with Countryid. CountryID: "+entity.CountryId + " ID:"+id);
                }
                var unUpdatedEntity = await countryRepository.GetByIdAsync(id);
                if (unUpdatedEntity == null)
                {
                    return new ResponseEntity("No Data");
                }
                var tempEntity = _mapper.Map<CountryDto, Country>(entity);
                countryRepository.Update(tempEntity);
                await _unitofWork.CommitAsync();
                return new ResponseEntity(entity);
            }
            catch (Exception e)
            {
                return new ResponseEntity("Update Error");
            }
        }
    }
}
