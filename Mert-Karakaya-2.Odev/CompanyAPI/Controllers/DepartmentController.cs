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
using CompanyAPI.Service.Services.Abstract;
using Microsoft.Extensions.Logging;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController<DepartmentDto, Department>
    {
        private readonly IDepartmentService _service;
        private readonly ICountryService _countryService;
        private readonly ILogger<DepartmentController> _logger;
        public DepartmentController(IDepartmentService departmentService, ICountryService countryService, ILogger<DepartmentController> logger) : base(departmentService)
        {
            _service = departmentService;
            _logger = logger;
            _countryService = countryService;
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
            _logger.LogInformation($"Get a Department with Id is {id}.");

            return await base.GetByIdAsync(id);
        }

        [HttpPost]
        public new async Task<IActionResult> CreateAsync([FromBody] DepartmentDto department)
        {
            _logger.LogInformation($"Created a Department.");

            var validationResult = Validator.DepartmentValidator(department,_countryService);
            if (!string.IsNullOrWhiteSpace(validationResult))
            {
                return BadRequest(new ResponseEntity(validationResult));
            }

            var insertResult = await _service.InsertAsync(department);

            if (!insertResult.isSuccess)
                return BadRequest(insertResult);

            return StatusCode(201, insertResult);
        }

        [HttpPut("{id:int}")]
        public new async Task<IActionResult> UpdateAsync(int id, [FromBody] DepartmentDto department)
        {
            _logger.LogInformation($"Update a Department with Id is {id}.");

            var validationResult = Validator.DepartmentValidator(department, _countryService);
            if (!string.IsNullOrWhiteSpace(validationResult))
            {
                return BadRequest(new ResponseEntity(validationResult));
            }

            return await base.UpdateAsync(id, department);
        }


        [HttpDelete("{id:int}")]
        public new async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Delete a Department with Id is {id}.");

            return await base.DeleteAsync(id);
        }
    }
}
