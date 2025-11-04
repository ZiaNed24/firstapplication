using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FirstApplication.Models;
using FirstApplication.Repository.Interfaces;

namespace FirstApplication.Repositories
{
    public class DepartmentRepo :IDepartmentRepo
    {
        private readonly CompanyManagementContext _context;

        public DepartmentRepo(CompanyManagementContext context)
        {
            _context = context;
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }
        public async Task<Department?> GetDepartById(int id)
        {
            return await _context.Departments.FirstOrDefaultAsync(e => e.DepartmentId == id);
        }

    }
}
