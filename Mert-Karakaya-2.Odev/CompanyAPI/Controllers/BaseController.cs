using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Core;
using Microsoft.Extensions.Logging;

namespace CompanyAPI.Controllers
{
    [ApiController]
    public class BaseController<Dto,T> : ControllerBase
    {
        private readonly IBaseService<Dto,T> _baseService;
        public BaseController(IBaseService<Dto, T> baseService)
        {
            _baseService = baseService;
        }

        [Route("GetAll")]
        [HttpGet]
        public virtual async Task<IActionResult> GetAllAsync()
        {
            var result = await _baseService.GetAllAsync();
            if (!result.isSuccess)
                return BadRequest(result);
            if (result.data == null)
                return NoContent();

            return Ok(result);
        }
        [NonAction]
        public virtual async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _baseService.GetByIdAsync(id);

            if (!result.isSuccess)
                return BadRequest(result);

            if (result.data is null)
                return NoContent();

            return Ok(result);
        }

        [NonAction]
        public virtual async Task<IActionResult> CreateAsync([FromBody] Dto entity)
        {
            var result = await _baseService.InsertAsync(entity);

            if (result.isSuccess)
                return StatusCode(201, result);

            return BadRequest(result);
        }

        [NonAction]
        public virtual async Task<IActionResult> UpdateAsync(int id, [FromBody] Dto entity)
        {
            var result = await _baseService.UpdateAsync(id, entity);

            if (result.isSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [NonAction]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _baseService.DeleteAsync(id);

            if (result.isSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
