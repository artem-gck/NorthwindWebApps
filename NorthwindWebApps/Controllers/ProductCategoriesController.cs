using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.DataAccess.Products;
using Northwind.Services;
using Northwind.Services.Models;

namespace NorthwindWebApps.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class ProductCategoriesController : ControllerBase
    {
        /// <summary>
        /// The picture service.
        /// </summary>
        private IProductCategoryPicturesService _pictureService;

        /// <summary>
        /// The category service.
        /// </summary>
        private IProductCategoryManagementService _categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoriesController"/> class.
        /// </summary>
        /// <param name="pictureService">The picture service.</param>
        /// <param name="categoryService">The category service.</param>
        public ProductCategoriesController(IProductCategoryPicturesService pictureService, IProductCategoryManagementService categoryService)
            => (_pictureService, _categoryService) = (pictureService, categoryService);

        // POST api/categories
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(ProductCategory category)
        {
            if (_categoryService.CreateCategory(category) == -1)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Create), new { id = category.Id }, category);
        }

        // GET api/categories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<ProductCategory>> GetAll()
        {
            var category = _categoryService.ShowCategories(0, int.MaxValue) as List<ProductCategory>;

            if (category is null)
            {
                return BadRequest();
            }

            return category;
        }

        // GET api/categories/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductCategory> Get(int id)
        {
            ProductCategory category;

            if (!_categoryService.TryShowCategory(id, out category))
            {
                return NotFound();
            }

            return category;
        }

        // PUT api/categories/2
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int id, ProductCategory category)
        {
            ProductCategory cat;

            if (id != category.Id)
            {
                return BadRequest();
            }

            if (!_categoryService.TryShowCategory(id, out cat))
            {
                return BadRequest();
            }

            _categoryService.UpdateCategories(id, category);

            return NoContent();
        }

        // DELETE api/categories/2
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            ProductCategory cat;

            if (!_categoryService.TryShowCategory(id, out cat))
            {
                return BadRequest();
            }

            _categoryService.DestroyCategory(id);

            return NoContent();
        }

        // PUT api/categories/2/picture
        [HttpPut("{id}/picture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdatePicture(int id, IFormFile file)
        {
            var size = file.Length;

            if (file.Length > 0)
            {
                _pictureService.UpdatePicture(id, file.OpenReadStream());
            }

            return Ok(new { size });
        }

        // GET api/categories/2/picture
        [HttpGet("{id}/picture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Stream> GetPicture(int id)
        {
            byte[] picture;

            if(_pictureService.TryShowPicture(id, out picture))
            {
                return new MemoryStream(picture);
            }

            return BadRequest(); 
        }

        // DELETE api/categories/2/picture
        [HttpDelete("{id}/picture")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeletePicture(int id)
        {
            ProductCategory cat;

            if (!_categoryService.TryShowCategory(id, out cat))
            {
                return BadRequest();
            }

            _pictureService.DestroyPicture(id);

            return NoContent();
        }
    }
}
