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
    public class FolderService : BaseService<FolderDto,Folder>, IFolderService
    {
        private readonly IFolderRepository folderRepository;
        private readonly IUnitofWork _unitofWork;
        public FolderService(IFolderRepository repository, IUnitofWork unitofWork, IMapper mapper) : base(repository, unitofWork, mapper)
        {
            folderRepository = repository;
            _unitofWork = unitofWork;
        }
        public override async Task<ResponseEntity> UpdateAsync(int id, FolderDto entity)
        {
            try
            {
                if (id != entity.FolderId)
                {
                    return new ResponseEntity("Not Equal ID with Folder ID. FolderID: " + entity.FolderId + " ID:" + id);
                }
                var unUpdatedEntity = await folderRepository.GetByIdAsync(id);
                if (unUpdatedEntity == null)
                {
                    return new ResponseEntity("No Data");
                }
                var tempEntity = _mapper.Map<FolderDto, Folder>(entity);
                folderRepository.Update(tempEntity);
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
