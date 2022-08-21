using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Core;
using CompanyAPI.Core.Entities;
using CompanyAPI.Data.DTO;
using CompanyAPI.Data.Model;
using CompanyAPI.Helpers;
using CompanyAPI.Service.Services;
using Microsoft.Extensions.Logging;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : BaseController<CountryDto,Country>
    {
        private readonly ICountryService _service;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryService countryService, ILogger<CountryController> logger) : base(countryService)
        {
            _service = countryService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Get Method");

            var result = await _service.GetAllAsync();

            if (!result.isSuccess)
                return BadRequest(result);

            if (result.data is null)
                return NoContent();

            return Ok(result);
        }


        [HttpGet("{id:int}")]
        public new async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Get a Country with Id is {id}.");

            return await base.GetByIdAsync(id);
        }

        [HttpPost]
        public new async Task<IActionResult> CreateAsync([FromBody] CountryDto country)
        {
            _logger.LogInformation($"Created a Country.");

            var validationResult = Validator.CountryValidator(country);
            if (!string.IsNullOrWhiteSpace(validationResult))
            {
                return BadRequest(new ResponseEntity(validationResult));
            }

            var insertResult = await _service.InsertAsync(country);

            if (!insertResult.isSuccess)
                return BadRequest(insertResult);

            return StatusCode(201, insertResult);
        }

        [HttpPut("{id:int}")]
        public new async Task<IActionResult> UpdateAsync(int id, [FromBody] CountryDto country)
        {
            _logger.LogInformation($"Update a Country with Id is {id}.");

            var validationResult = Validator.CountryValidator(country);
            if (!string.IsNullOrWhiteSpace(validationResult))
            {
                return BadRequest(new ResponseEntity(validationResult));
            }

            return await base.UpdateAsync(id, country);
        }


        [HttpDelete("{id:int}")]
        public new async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Delete a Country with Id is {id}.");

            return await base.DeleteAsync(id);
        }
    }
}
