using Microsoft.AspNetCore.Mvc;
using Northwind.DataAccess.Products;
using Northwind.Services;
using Northwind.Services.Models;

namespace NorthwindWebApps.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        /// <summary>
        /// IProductManagementService servise.
        /// </summary>
        private IProductManagementService _service;

        /// <summary>
        /// ProductsController constructor.
        /// </summary>
        /// <param name="service">IProductManagementService service.</param>
        public ProductsController(IProductManagementService service)
            => _service = service;

        // POST api/products
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(Product product)
        {
            if (_service.CreateProduct(product) == -1)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Create), new { id = product.Id }, product);
        }

        // GET api/products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            var products = _service.ShowProducts(0, int.MaxValue) as List<Product>;

            if (products is null)
            {
                return BadRequest();
            }

            return products;
        }

        // GET api/products/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> Get(int id)
        {
            Product product;

            if (!_service.TryShowProduct(id, out product))
            {
                return NotFound();
            }

            return product;
        }

        // PUT api/products/2
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Update(int id, Product product)
        {
            Product pro;

            if (id != product.Id)
            {
                return BadRequest();
            }

            if (!_service.TryShowProduct(id, out pro))
            {
                return BadRequest();
            }

            _service.UpdateProduct(id, product);

            return NoContent();
        }

        // DELETE api/products/2
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            Product pro;

            if (!_service.TryShowProduct(id, out pro))
            {
                return BadRequest();
            }

            _service.DestroyProduct(id);

            return NoContent();
        }
    }
}
