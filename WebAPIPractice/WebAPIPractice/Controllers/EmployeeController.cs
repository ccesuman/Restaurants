using BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIPractice.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;

        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var result = _employee.GetEmployeeById(id);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var result = _employee.GetAllEmployees();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                Name = dto.Name,
                Department = dto.Department, 
                Salary = dto.Salary
            };
            _employee.AddEmployee(employee);

            return CreatedAtAction(nameof(GetEmployeeById), new { Id = employee.Id }, employee);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveEmployee(int id)
        {
            var employee = _employee.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
             _employee.RemoveEmployee(employee);
             return NoContent();
        }
    }
}
