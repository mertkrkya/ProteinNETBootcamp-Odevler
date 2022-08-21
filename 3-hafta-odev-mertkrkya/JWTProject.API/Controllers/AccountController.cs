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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountService service, ILogger<AccountController> logger)
        {
            _service = service;
            _logger = logger;
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
            _logger.LogInformation($"Get a Account with Id is {id}.");

            var result = await _service.GetByIdAsync(id);
            if (!result.isSuccess)
                return BadRequest(result);
            if (result.data == null)
                return NoContent();

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AccountDto entity)
        {
            var validationResult = Validator.Validator.AccountValidator(entity);
            if (!string.IsNullOrWhiteSpace(validationResult))
            {
                return BadRequest(new ResponseEntity(validationResult));
            }
            var result = await _service.InsertAsync(entity);

            if (!result.isSuccess)
                return BadRequest(result);

            _logger.LogInformation($"Created a Account.");
            return StatusCode(201, result);

        }
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AccountDto entity)
        {
            int accountId = -1;
            var controlAccountId = User.Claims.FirstOrDefault(r => r.Type == "AccountId");
            if (controlAccountId == null)
                return BadRequest(new ResponseEntity("Öngörülemeyen bir hata meydana geldi."));
            accountId = Convert.ToInt32(controlAccountId.Value);
            if (accountId != id)
                return BadRequest(new ResponseEntity("Yetkisiz Account"));
            var validationResult = Validator.Validator.AccountValidator(entity);
            if (!string.IsNullOrWhiteSpace(validationResult))
            {
                return BadRequest(new ResponseEntity(validationResult));
            }
            var result = await _service.UpdateAsync(id, entity);

            if (!result.isSuccess)
                return BadRequest(result);

            _logger.LogInformation($"Update a Account {User.Identity.Name}.");
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.isSuccess)
                return BadRequest(result);

            _logger.LogInformation($"Delete a Account with Id is {id}.");
            return Ok(result);
            
        }
    }
}
