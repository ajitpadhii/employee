using SampleWebApi.Models;

namespace SampleWebApi.Repositories

{
    public interface IEmployeeRepository
    {
        Task<Employee>AddEmployeeAsync(Employee employee);
        Task<Employee?>GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    }
}
