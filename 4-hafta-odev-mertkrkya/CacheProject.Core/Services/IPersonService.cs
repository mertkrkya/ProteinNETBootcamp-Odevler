using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CacheProject.Core.Dto;
using CacheProject.Core.Entities;
using CacheProject.Core.Models;


namespace CacheProject.Core.Services
{
    public interface IPersonService : IBaseService<PersonDto,Person>
    {

    }
}
