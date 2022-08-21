using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JWTProject.Core;
using JWTProject.Core.Dto;
using JWTProject.Core.Entities;
using JWTProject.Core.Models;
using JWTProject.Core.Repositories;
using JWTProject.Core.Services;
using JWTProject.Core.UnitofWork;

namespace JWTProject.Service.Services
{
    public class AccountService : BaseService<AccountDto,Account>, IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IUnitofWork _unitofWork;
        public AccountService(IAccountRepository repository, IUnitofWork unitofWork, IMapper mapper) : base(repository, unitofWork, mapper)
        {
            accountRepository = repository;
            _unitofWork = unitofWork;
        }
        public override async Task<ResponseEntity> InsertAsync(AccountDto entity)
        {
            try
            {
                if(entity.Id != 0)
                    return new ResponseEntity("Id 0'dan farklı değer girilemez.");
                var tempEntity = _mapper.Map<AccountDto, Account>(entity);
                tempEntity.LastActivity = DateTime.Now;;
                tempEntity.CreationTime = DateTime.Now;
                tempEntity.ModTime = DateTime.Now;
                var result = accountRepository.InsertAsync(tempEntity);
                await _unitofWork.CommitAsync();
                return new ResponseEntity(entity);
            }
            catch (Exception e)
            {
                return new ResponseEntity("Save Error");
            }
        }
        public override async Task<ResponseEntity> UpdateAsync(int id, AccountDto entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    return new ResponseEntity("Not Equal ID. Parameter ID: " + id + " ID:" + entity.Id);
                }
                var unUpdatedEntity = await accountRepository.GetByIdAsync(id);
                if (unUpdatedEntity == null)
                {
                    return new ResponseEntity("No Data");
                }
                var tempEntity = _mapper.Map<AccountDto, Account>(entity);
                tempEntity.ModTime = DateTime.Now;
                accountRepository.Update(tempEntity);
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
