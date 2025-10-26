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

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var emp=await _employeeService.GetAllEmployeesAsync();
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
    }
}
