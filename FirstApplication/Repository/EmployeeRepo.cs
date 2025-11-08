using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FirstApplication.Models;

namespace FirstApplication.Repositories
{
    public class EmployeeRepository : IEmployeeRepo
    {
        private readonly CompanyManagementContext _context;

        public EmployeeRepository(CompanyManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                           .Include(e => e.Job)
                           .Include(e => e.Department)
                           .Include(e => e.Manager)
                           .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
        }
        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

    }
}
