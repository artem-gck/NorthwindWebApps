using Microsoft.AspNetCore.Mvc;
using Northwind.Services;
using Northwind.Services.Models;

namespace NorthwindWebApps.Controllers
{
    [ApiController]
    [Route("api/suppliers")]
    public class SuppliersController : Controller
    {
        /// <summary>
        /// IProductManagementService servise.
        /// </summary>
        private ISupplierManagementService _service;

        /// <summary>
        /// ProductsController constructor.
        /// </summary>
        /// <param name="service">IProductManagementService service.</param>
        public SuppliersController(ISupplierManagementService service)
            => _service = service;

        // GET api/suppliers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAllAsync()
        {
            var suppliers = await _service.ShowSuppliersAsync(0, int.MaxValue) as List<Supplier>;

            if (suppliers is null)
            {
                return BadRequest();
            }

            return suppliers;
        }

        // GET api/suppliers/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Supplier>> GetAsync(int id)
        {
            Supplier supplier;

            if (!_service.TryShowProduct(id, out supplier))
            {
                return NotFound();
            }

            return supplier;
        }
    }
}
