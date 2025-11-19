using AutoMapper;
using FirstApplication.DTOs;
using FirstApplication.Models;
using FirstApplication.Services;
using FirstApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        public DepartmentsController(IDepartmentService departService, IMapper mapper)
        {
            _departmentService = departService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> GetDeptById(int id)
        {
            var emp = await _departmentService.GetDepartmentById(id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var dep = _mapper.Map<Department>(dto);

            var createdDept = await _departmentService.AddDepartmentAsync(dep);

            return CreatedAtAction(nameof(GetDeptById), new { id = createdDept.DepartmentId }, createdDept);
        }

    }
}
