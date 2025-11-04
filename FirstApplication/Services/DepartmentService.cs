using FirstApplication.Models;
using FirstApplication.Repositories;
using FirstApplication.Repository.Interfaces;
using FirstApplication.Services.Interfaces;

namespace FirstApplication.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepo _departRepository;

        public DepartmentService(IDepartmentRepo departmentRepo)
        {
            _departRepository = departmentRepo;
        }
        public async Task<Department> AddDepartmentAsync(Department department)
        {
            if (string.IsNullOrEmpty(department.DepartmentName))
            {
                throw new ArgumentNullException("Department cannot be empty");
            }
            return await _departRepository.AddDepartmentAsync(department);
        }
        public async Task<Department?> GetDepartmentById(int id)
        {
            return await _departRepository.GetDepartById(id);
        }
    }
}
