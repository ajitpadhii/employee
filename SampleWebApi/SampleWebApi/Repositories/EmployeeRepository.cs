using SampleWebApi.Models;
namespace SampleWebApi.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private static readonly List<Employee> _employees = new List<Employee>();
        private static int _nextId = 0;
        private readonly ILogger<EmployeeRepository> _logger;
        private static readonly object _lock = new object();

        public EmployeeRepository(ILogger<EmployeeRepository> logger)
        {
            _logger = logger;
        }
        public Task<Employee> AddEmployeeAsync(Employee employee)
        {
            lock(_lock)
            {
                employee.Id = Interlocked.Increment(ref _nextId);
                _employees.Add(employee);
            }
           _logger.LogInformation("Added employee with ID {EmployeeId}", employee.Id);
            return Task.FromResult(employee);
            // Code to add employee to database
        }
        public Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            Employee? employee;
            lock (_lock)
            {
                employee = _employees.FirstOrDefault(e => e.Id == id);

            }
            return Task.FromResult(employee);
            // Code to retrieve employee from database by ID
        }
        public Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            lock (_lock)
            {
                return Task.FromResult(_employees.AsEnumerable());

            }
        }
    }
}
