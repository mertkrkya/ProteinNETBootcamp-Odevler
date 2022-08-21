using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CacheProject.Core.Dto;
using CacheProject.Core.Entities;
using CacheProject.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

namespace JWTProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;
        private readonly ILogger<PersonController> _logger;
        private readonly IMemoryCache _memoryCache;
        public PersonController(IPersonService service, ILogger<PersonController> logger, IMemoryCache memoryCache)
        {
            _service = service;
            _logger = logger;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public async Task<IActionResult> GetByPaginationAsync([FromQuery] string key, [FromQuery] int pageNum, [FromQuery] int pageSize)
        {
            _logger.LogInformation($"Pagination with key: {key}");
            ResponseEntity response = null;
            if(!_memoryCache.TryGetValue<ResponseEntity>(key,out response))
            {
                response = await _service.GetAllAsync();
                if (!response.isSuccess)
                    return BadRequest(response);
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                    Priority = CacheItemPriority.Normal
                };
                _memoryCache.Set(key, response,options);
            }
            if(response != null && response.data != null)
            {
                if (pageNum < 0)
                    pageNum = 1;
                try
                {
                    var responseData = (List<PersonDto>)response.data;
                    var resultData = responseData.Skip((pageNum - 1) * pageNum).Take(pageSize).ToList();
                    return Ok(new ResponseEntity(resultData));
                }
                catch (Exception e)
                {
                    string errorMessage = $"Type cast error. Error Message: " + e.Message;
                    _logger.LogError(errorMessage);
                    return BadRequest(new ResponseEntity(errorMessage));
                }
            }
            return NoContent();
        }
    }
}
