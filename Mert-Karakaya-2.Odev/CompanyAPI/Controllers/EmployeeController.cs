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
    public class EmployeeController : BaseController<EmployeeDto, Employee>
    {
        private readonly IEmployeeService _service;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IDepartmentService _departmentService;
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService,ILogger<EmployeeController> logger) : base(employeeService)
        {
            _service = employeeService;
            _logger = logger;
            _departmentService = departmentService;
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
            _logger.LogInformation($"Get a Employee with Id is {id}.");

            return await base.GetByIdAsync(id);
        }

        [HttpPost]
        public new async Task<IActionResult> CreateAsync([FromBody] EmployeeDto employee)
        {
            _logger.LogInformation($"Created a Employee.");

            var validationResult = Validator.EmployeeValidator(employee, _departmentService);
            if (!string.IsNullOrWhiteSpace(validationResult))
            {
                return BadRequest(new ResponseEntity(validationResult));
            }

            var insertResult = await _service.InsertAsync(employee);

            if (!insertResult.isSuccess)
                return BadRequest(insertResult);

            return StatusCode(201, insertResult);
        }

        [HttpPut("{id:int}")]
        public new async Task<IActionResult> UpdateAsync(int id, [FromBody] EmployeeDto employee)
        {
            _logger.LogInformation($"Update a Employee with Id is {id}.");
            var validationResult = Validator.EmployeeValidator(employee, _departmentService);
            if (!string.IsNullOrWhiteSpace(validationResult))
            {
                return BadRequest(new ResponseEntity(validationResult));
            }
            return await base.UpdateAsync(id, employee);
        }


        [HttpDelete("{id:int}")]
        public new async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Delete a Employee with Id is {id}.");

            return await base.DeleteAsync(id);
        }
    }
}
