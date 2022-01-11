using Microsoft.AspNetCore.Mvc;
using Northwind.Services;
using Northwind.Services.Blogging;
using Northwind.Services.Models;

namespace NorthwindWebApps.Controllers
{
    [ApiController]
    [Route("api/blogging")]
    public class BlogArticlesController : Controller
    {
        /// <summary>
        /// The service.
        /// </summary>
        private IBloggingService _service;

        /// <summary>
        /// The employee management service.
        /// </summary>
        private IEmployeeManagementService _employeeManagementService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogArticlesController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public BlogArticlesController(IBloggingService service, IEmployeeManagementService employeeManagementService)
            => (_service, _employeeManagementService) = (service, employeeManagementService);

        // POST api/blogging
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(BlogArticle article)
        {
            article.DatePublished = DateTime.Now;
            Employee employee;

            if (!_employeeManagementService.TryShowEmployee(article.PublisherId, out employee))
            {
                return BadRequest();
            }

            int? employeeId = await _service.CreateArticleAsync(article);

            if (employeeId is null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(CreateAsync), new { id = employeeId }, article);
        }

        // GET api/blogging
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BlogRead>>> GetAllAsync()
        {
            var articles = await _service.ShowArticlesAsync(0, int.MaxValue) as List<BlogArticle>;

            if (articles is null)
            {
                return BadRequest();
            }

            var ans = new List<BlogRead>();

            foreach (var item in articles)
            {
                Employee employee;

                _employeeManagementService.TryShowEmployee(item.PublisherId, out employee);

                var blog = new BlogRead()
                {
                    Id = item.Id,
                    Title = item.Title,
                    DatePublished = item.DatePublished,
                    PublisherId = item.PublisherId,
                    PublisherName = employee.FirstName + " " + employee.LastName + ", " + employee.Title,
                };

                ans.Add(blog);
            }

            return ans;
        }

        // GET api/blogging/2
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BlogReadId>> GetAsync(int id)
        {
            BlogArticle article;

            if (!_service.TryShowArticle(id, out article))
            {
                return BadRequest();
            }

            Employee employee;

            _employeeManagementService.TryShowEmployee(article.PublisherId, out employee);

            var blog = new BlogReadId()
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.Text,
                DatePublished = article.DatePublished,
                PublisherId = article.PublisherId,
                PublisherName = employee.FirstName + " " + employee.LastName + ", " + employee.Title,
            };

            return blog;
        }

        // DELETE api/blogging/2
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            BlogArticle art;

            if (!_service.TryShowArticle(id, out art))
            {
                return BadRequest();
            }

            await _service.DestroyArticleAsync(id);

            return NoContent();
        }

        // PUT api/blogging/2
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync(int id, BlogArticle article)
        {
            BlogArticle blog;

            if (!_service.TryShowArticle(id, out blog))
            {
                return BadRequest();
            }

            blog.Text = article.Text;
            blog.Title = article.Title;

            await _service.UpdateArticleAsync(id, blog);

            return NoContent();
        }
    }
}
