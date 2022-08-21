using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTProject.Core.Dto;
using JWTProject.Core.Entities;
using JWTProject.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace JWTProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;
        private readonly ILogger<PersonController> _logger;
        public PersonController(IPersonService service, ILogger<PersonController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("GetPeopleByAccountId")]
        public async Task<IActionResult> GetPeopleByAccountId()
        {
            int accountId = -1;
            var controlAccountId = User.Claims.FirstOrDefault(r => r.Type == "AccountId");
            if (controlAccountId == null)
                return BadRequest(new ResponseEntity("Öngörülemeyen bir hata meydana geldi."));
            accountId = Convert.ToInt32(controlAccountId.Value);
            var result = await _service.GetPeopleByAccountIdAsync(accountId);
            if (!result.isSuccess)
                return BadRequest(result);
            _logger.LogInformation($"{User.Identity.Name} kullanıcısının person nesneleri alındı.");
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            if (!result.isSuccess)
                return BadRequest(result);
            if (result.data == null)
                return NoContent();

            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Get a Person with Id is {id}.");

            var result = await _service.GetByIdAsync(id);
            if (!result.isSuccess)
                return BadRequest(result);
            if (result.data == null)
                return NoContent();

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PersonDto entity)
        {
            int accountId = -1;
            var controlAccountId = User.Claims.FirstOrDefault(r => r.Type == "AccountId");
            if (controlAccountId == null)
                return BadRequest(new ResponseEntity("Öngörülemeyen bir hata meydana geldi."));
            accountId = Convert.ToInt32(controlAccountId.Value);
            var validationResult = Validator.Validator.PersonValidator(entity, accountId);
            if (!string.IsNullOrWhiteSpace(validationResult))
            {
                return BadRequest(new ResponseEntity(validationResult));
            }
            var result = await _service.InsertAsync(entity);

            if (!result.isSuccess)
                return BadRequest(result);

            _logger.LogInformation($"Created a Person.");
            return StatusCode(201, result);

        }
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PersonDto entity)
        {
            int accountId = -1;
            var controlAccountId = User.Claims.FirstOrDefault(r => r.Type == "AccountId");
            if (controlAccountId == null)
                return BadRequest(new ResponseEntity("Öngörülemeyen bir hata meydana geldi."));
            accountId = Convert.ToInt32(controlAccountId.Value);
            var validationResult = Validator.Validator.PersonValidator(entity, accountId);
            if (!string.IsNullOrWhiteSpace(validationResult))
            {
                return BadRequest(new ResponseEntity(validationResult));
            }
            var result = await _service.UpdateAsync(id, entity);

            if (!result.isSuccess)
                return BadRequest(result);

            _logger.LogInformation($"Update a Person {User.Identity.Name}.");
            return Ok(result);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            int accountId = -1;
            var controlAccountId = User.Claims.FirstOrDefault(r => r.Type == "AccountId");
            if (controlAccountId == null)
                return BadRequest(new ResponseEntity("Öngörülemeyen bir hata meydana geldi."));
            accountId = Convert.ToInt32(controlAccountId.Value);
            var validationResult = Validator.Validator.PersonDeleteValidator(id, accountId);
            if (!string.IsNullOrWhiteSpace(validationResult))
            {
                return BadRequest(new ResponseEntity(validationResult));
            }
            var result = await _service.DeleteAsync(id);

            if (!result.isSuccess)
                return BadRequest(result);

            _logger.LogInformation($"Delete a Person with Id is {id}.");
            return Ok(result);

        }
    }
}
