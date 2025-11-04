using System.Collections.Generic;
using System.Threading.Tasks;
using FirstApplication.Models;

namespace FirstApplication.Repositories
{
    public interface IEmployeeRepo
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<Employee> AddEmployeeAsync(Employee employee);

    }
}
