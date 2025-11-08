using AutoMapper;
using FirstApplication.DTOs;
using FirstApplication.Models;
using FirstApplication.Services;
using FirstApplication.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService,IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var emp=await _employeeService.GetAllEmployeesAsync();
            if (emp == null || !emp.Any())
                return NotFound("No employees found.");

            return Ok(emp);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetEmpById(int id)
        {
            var emp = await _employeeService.GetEmployeeByIdAsync(id);
            if (emp == null) {
                return NotFound(); 
            }
            return Ok(emp);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // map DTO → entity
            var employee = _mapper.Map<Employee>(dto);

            var createdEmployee = await _employeeService.AddEmployeeAsync(employee);

            return CreatedAtAction(nameof(GetEmpById), new { id = createdEmployee.EmployeeId }, createdEmployee);
        }
    }
}
