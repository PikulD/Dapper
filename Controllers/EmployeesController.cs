using EmployeesAPI.Models;
using EmployeesAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployees(int id)
        {
            return await _employeeRepository.Get(id);
        }
        [HttpGet("/Company/{id}")]
        public async Task<IActionResult> GetEmployeesCompany(int id)
        {
            try
            {
                var companies = await _employeeRepository.GetCompany(id);
                return Ok(companies);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }   
        }
        [HttpGet("/Department/{id}")]
        public async Task<IActionResult> GetEmployeesDepartment(int id)
        {
            try
            {
                var companies = await _employeeRepository.GetDepartment(id);
                return Ok(companies);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<int> PostEmployees([FromBody] Employee employee)
        {
            var newEmployee = await _employeeRepository.Create(employee);
            return newEmployee.Id;
        }

        [HttpPut]
        public async Task<ActionResult> PutEmployees(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            await _employeeRepository.Update(employee);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var employeeToDelete = await _employeeRepository.Get(id);
            if (employeeToDelete == null)
                return NotFound();

            await _employeeRepository.Delete(employeeToDelete.Id);
            return NoContent();
        }
    }
}
