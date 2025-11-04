using FirstApplication.Models;

namespace FirstApplication.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<Department> AddDepartmentAsync(Department department);
        Task<Department?> GetDepartmentById(int id);

    }
}
