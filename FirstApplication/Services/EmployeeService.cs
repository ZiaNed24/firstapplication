using FirstApplication.Models;
using FirstApplication.Repositories;
using FirstApplication.Services.Interfaces;

namespace FirstApplication.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepository;

        public EmployeeService(IEmployeeRepo employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllEmployeesAsync();
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
