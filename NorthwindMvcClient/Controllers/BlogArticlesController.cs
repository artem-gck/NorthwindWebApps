using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Northwind.Services.Blogging;
using Northwind.Services.Models;
using NorthwindMvcClient.ViewModels;
using System.Net.Http.Headers;

namespace NorthwindMvcClient.Controllers
{
    public class BlogArticlesController : Controller
    {
        public const int PageSize = 5;

        private HttpClient _httpClient;

        public BlogArticlesController(IConfiguration configuration)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri($"https://localhost:{configuration["port"]}/api/")
            };

            this._httpClient.DefaultRequestHeaders.Accept.Clear();
            this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index(int articlePage = 1)
        {
            var articlesJson = await this._httpClient.GetStringAsync("articles/");
            var articles = JsonConvert.DeserializeObject<List<BlogRead>>(articlesJson);

            var employeesJson = await this._httpClient.GetStringAsync("Employees/");
            var employees = JsonConvert.DeserializeObject<List<Employee>>(employeesJson);

            var articlesList = articles.Select(article => new BlogArticleViewModel()
            {
                Id = article.Id,
                Title = article.Title,
                DatePublished = article.DatePublished,
                PublisherId = article.PublisherId,
            });

            var employeesList = employees.Select(employee => new EmployeeViewModel()
            {
                Id = employee.Id,
                LastName = employee.LastName,
            }).ToList();

            ViewData["employees"] = employeesList;

            return View(new BlogArticlesListViewModel
            {
                BlogArticles = articlesList
                    .OrderBy(art => art.Id)
                    .Skip((articlePage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = articlePage,
                    ItemsPerPage = PageSize,
                    TotalItems = articles.Count()
                }
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var articleJson = await this._httpClient.GetStringAsync($"articles/{id}");
            var article = JsonConvert.DeserializeObject<BlogReadId>(articleJson);

            var productsArticleJson = await this._httpClient.GetStringAsync($"articles/{id}/products");
            var productsArticle = JsonConvert.DeserializeObject<BlogArticleProductShow>(productsArticleJson);

            var products = productsArticle.Products.Select(product => new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
            });

            var articleView = new BlogArticleViewModel()
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.Text,
                DatePublished = article.DatePublished,
                PublisherId = article.PublisherId,
                Products = products,
            };

            ViewData["employees"] = article.PublisherName;

            return View(articleView);
        }

        public async Task<IActionResult> Update(int id)
        {
            var articleJson = await this._httpClient.GetStringAsync($"articles/{id}");
            var article = JsonConvert.DeserializeObject<BlogReadId>(articleJson);

            var employeesJson = await this._httpClient.GetStringAsync("Employees/");
            var employees = JsonConvert.DeserializeObject<List<Employee>>(employeesJson);

            var productsArticleJson = await this._httpClient.GetStringAsync($"articles/{id}/products");
            var productsArticle = JsonConvert.DeserializeObject<BlogArticleProductShow>(productsArticleJson);

            var products = productsArticle.Products.Select(product => new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
            });

            ViewBag.employees = new SelectList(employees, "Id", "LastName");

            var articleView = new BlogArticleViewModel()
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.Text,
                PublisherId = article.PublisherId,
                Products = products,
            };

            return View(articleView);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BlogArticleViewModel article)
        {
            var articleServ = new BlogArticle()
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.Text,
                PublisherId = article.PublisherId
            };

            await this._httpClient.PutAsJsonAsync($"articles/{article.Id}", articleServ);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var articleJson = await this._httpClient.GetStringAsync($"articles/{id}");
            var article = JsonConvert.DeserializeObject<BlogReadId>(articleJson);

            var articleView = new BlogArticleViewModel()
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.Text,
                DatePublished = article.DatePublished,
                PublisherId = article.PublisherId
            };

            ViewData["employee"] = article.PublisherName;

            return View(articleView);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BlogArticleViewModel article)
        {
            await this._httpClient.DeleteAsync($"articles/{article.Id}");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            var employeesJson = await this._httpClient.GetStringAsync("Employees/");
            var employees = JsonConvert.DeserializeObject<List<Employee>>(employeesJson);

            ViewBag.employees = new SelectList(employees, "Id", "LastName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogArticleViewModel article)
        {
            var articleServ = new BlogArticle()
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.Text,
                PublisherId = article.PublisherId
            };

            await this._httpClient.PostAsJsonAsync("articles/", articleServ);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddProductToArticle(int id)
        {
            var productsJson = await this._httpClient.GetStringAsync("products/");
            var products = JsonConvert.DeserializeObject<List<Product>>(productsJson);

            var articleJson = await this._httpClient.GetStringAsync($"articles/{id}");
            var article = JsonConvert.DeserializeObject<BlogReadId>(articleJson);

            var productsArticleJson = await this._httpClient.GetStringAsync($"articles/{id}/products");
            var productsArticle = JsonConvert.DeserializeObject<BlogArticleProductShow>(productsArticleJson);

            var productsInArticle = productsArticle.Products.Select(product => new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
            });

            var articleView = new BlogArticleViewModel()
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.Text,
                DatePublished = article.DatePublished,
                PublisherId = article.PublisherId,
                Products = productsInArticle,
            };

            ViewBag.products = new SelectList(products, "Id", "Name");

            return View(articleView);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToArticle(BlogArticleViewModel article)
        {
            await this._httpClient.PostAsync($"articles/{article.Id}/products/{article.AddingProductId}", null);

            return RedirectToAction("Update", article);
        }

        public async Task<IActionResult> DeleteProductToArticle(int id, int id_del)
        {
            await this._httpClient.DeleteAsync($"articles/{id}/products/{id_del}");

            var articleJson = await this._httpClient.GetStringAsync($"articles/{id}");
            var article = JsonConvert.DeserializeObject<BlogReadId>(articleJson);

            var employeesJson = await this._httpClient.GetStringAsync("Employees/");
            var employees = JsonConvert.DeserializeObject<List<Employee>>(employeesJson);

            var productsArticleJson = await this._httpClient.GetStringAsync($"articles/{id}/products");
            var productsArticle = JsonConvert.DeserializeObject<BlogArticleProductShow>(productsArticleJson);

            var products = productsArticle.Products.Select(product => new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
            });

            ViewBag.employees = new SelectList(employees, "Id", "LastName");

            var articleView = new BlogArticleViewModel()
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.Text,
                PublisherId = article.PublisherId,
                Products = products,
            };

            return RedirectToAction("Update", articleView);
        }
    }
}