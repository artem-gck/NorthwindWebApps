using Microsoft.AspNetCore.Mvc;
using Northwind.Services;
using Northwind.Services.Models;

namespace NorthwindWebApps.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : Controller
    {
        /// <summary>
        /// IProductManagementService servise.
        /// </summary>
        private IEmployeeManagementService _service;

        /// <summary>
        /// ProductsController constructor.
        /// </summary>
        /// <param name="service">IProductManagementService service.</param>
        public EmployeesController(IEmployeeManagementService service)
            => _service = service;

        // POST api/employees
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(Employee employee)
        {
            if (await _service.CreateEmployeeAsync(employee) == -1)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(CreateAsync), new { id = employee.Id }, employee);
        }

        // GET api/employees
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllAsync()
        {
            var employees = await _service.ShowEmployeesAsync(0, int.MaxValue) as List<Employee>;

            if (employees is null)
            {
                return BadRequest();
            }

            return employees;
        }

        // GET api/employees/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Employee> Get(int id)
        {
            Employee employee;

            if (!_service.TryShowEmployee(id, out employee))
            {
                return NotFound();
            }

            return employee;
        }

        // PUT api/employees/2
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync(int id, Employee employee)
        {
            Employee emp;

            if (id != employee.Id)
            {
                return BadRequest();
            }

            if (!_service.TryShowEmployee(id, out emp))
            {
                return BadRequest();
            }

            await _service.UpdateEmployeeAsync(id, employee);

            return NoContent();
        }

        // DELETE api/employees/2
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Employee emp;

            if (!_service.TryShowEmployee(id, out emp))
            {
                return BadRequest();
            }

            await _service.DestroyEmployeeAsync(id);

            return NoContent();
        }
    }
}
