using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Repositories;
using SampleWebApi.Models;

namespace SampleWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository repo, ILogger<EmployeeController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            var created = await _repo.AddEmployeeAsync(employee);
            _logger.LogInformation("Created Employee with ID {EmployeeId}", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _repo.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                _logger.LogWarning("Employee with ID {EmployeeId} not found", id);
                return NotFound(new { message = $"Employee with ID {id} not found" });
            }
            return Ok(employee);
        }
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var employees = await _repo.GetAllEmployeesAsync();
            return Ok(employees);
        }
    }
}
