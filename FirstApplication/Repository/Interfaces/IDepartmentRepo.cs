using FirstApplication.Models;

namespace FirstApplication.Repository.Interfaces
{
    public interface IDepartmentRepo
    {
        Task<Department> AddDepartmentAsync(Department department);
        Task<Department?> GetDepartById(int id);

    }
}
