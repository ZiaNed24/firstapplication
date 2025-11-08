using AutoMapper;
using FirstApplication.DTOs;
using FirstApplication.Models;
using FirstApplication.Repositories;
using FirstApplication.Services.Interfaces;

namespace FirstApplication.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepo employeeRepository,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeResponseDto>> GetAllEmployeesAsync()
        {
            var emp= await _employeeRepository.GetAllEmployeesAsync();
            var res = _mapper.Map<IEnumerable<EmployeeResponseDto>>(emp);
            return res; 
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }
        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.FirstName))
            {
                throw new ArgumentNullException("employee cannot be empty");
            }
            return await _employeeRepository.AddEmployeeAsync(employee);
        }
    }
}
