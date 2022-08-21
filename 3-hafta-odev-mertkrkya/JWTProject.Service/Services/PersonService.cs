using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JWTProject.Service.Services;
using JWTProject.Core;
using JWTProject.Core.Dto;
using JWTProject.Core.Entities;
using JWTProject.Core.Models;
using JWTProject.Core.Repositories;
using JWTProject.Core.Services;
using JWTProject.Core.UnitofWork;

namespace JWTProject.Service.Services
{
    public class PersonService : BaseService<PersonDto,Person>, IPersonService
    {
        private readonly IPersonRepository personRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitofWork _unitofWork;
        public PersonService(IPersonRepository repository, IAccountRepository accountRepository, IUnitofWork unitofWork, IMapper mapper) : base(repository, unitofWork, mapper)
        {
            _accountRepository = accountRepository;
            personRepository = repository;
            _unitofWork = unitofWork;
        }
        public override async Task<ResponseEntity> InsertAsync(PersonDto entity)
        {
            try
            {
                var tempEntity = _mapper.Map<PersonDto, Person>(entity);
                tempEntity.CreationTime = DateTime.Now;
                tempEntity.ModTime = DateTime.Now;
                var result = personRepository.InsertAsync(tempEntity);
                await _unitofWork.CommitAsync();
                return new ResponseEntity(entity);
            }
            catch (Exception e)
            {
                return new ResponseEntity("Save Error");
            }
        }
        public override async Task<ResponseEntity> UpdateAsync(int id, PersonDto entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    return new ResponseEntity("Not Equal ID. Parameter ID: " + id + " ID:" + entity.Id);
                }
                var unUpdatedEntity = await personRepository.GetByIdAsync(id);
                if (unUpdatedEntity == null)
                {
                    return new ResponseEntity("No Data");
                }
                var tempEntity = _mapper.Map<PersonDto, Person>(entity);
                tempEntity.ModTime = DateTime.Now;
                personRepository.Update(tempEntity);
                await _unitofWork.CommitAsync();
                return new ResponseEntity(entity);
            }
            catch (Exception e)
            {
                return new ResponseEntity("Update Error");
            }
        }
        public async Task<ResponseEntity> GetPeopleByAccountIdAsync(int accountId)
        {
            //Buradaki parametre controller tarafında tokendan alınacak.
            var entityControl = await _accountRepository.GetByIdAsync(accountId);
            if (entityControl == null)
            {
                return new ResponseEntity("No Account Data");
            }
            try
            {
                var allPersonRecord = await personRepository.GetPeopleByAccountIdAsync(accountId);
                var mappedResult = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(allPersonRecord);
                return new ResponseEntity(mappedResult);
            }
            catch (Exception e)
            {
                return new ResponseEntity("Person Get People By Account Id Error");
            }
        }
        public override async Task<ResponseEntity> DeleteAsync(int id)
        {
            try
            {
                var deleteEntity = await personRepository.GetByIdAsync(id);
                if (deleteEntity == null)
                {
                    return new ResponseEntity("No Data");
                }
                personRepository.Delete(deleteEntity);
                _unitofWork.Commit();
                return new ResponseEntity(deleteEntity);
            }
            catch (Exception e)
            {
                return new ResponseEntity("Delete Error");
            }
        }
    }
}
