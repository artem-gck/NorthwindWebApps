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
        public IActionResult Create(Employee employee)
        {
            if (_service.CreateEmployee(employee) == -1)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Create), new { id = employee.Id }, employee);
        }

        // GET api/employees
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Employee>> GetAll()
        {
            var employees = _service.ShowEmployees(0, int.MaxValue) as List<Employee>;

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
        public IActionResult Update(int id, Employee employee)
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

            _service.UpdateEmployee(id, employee);

            return NoContent();
        }

        // DELETE api/employees/2
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            Employee emp;

            if (!_service.TryShowEmployee(id, out emp))
            {
                return BadRequest();
            }

            _service.DestroyEmployee(id);

            return NoContent();
        }
    }
}
