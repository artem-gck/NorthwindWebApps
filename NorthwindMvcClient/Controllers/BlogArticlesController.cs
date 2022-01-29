using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Northwind.Services.Blogging;
using NorthwindMvcClient.ViewModels;
using System.Net.Http.Headers;

namespace NorthwindMvcClient.Controllers
{
    public class BlogArticlesController : Controller
    {
        public const int PageSize = 5;

        private HttpClient _httpClient;

        public BlogArticlesController()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7041/api/articles/")
            };

            this._httpClient.DefaultRequestHeaders.Accept.Clear();
            this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index(int productPage = 1)
        {
            var json = await this._httpClient.GetStringAsync(string.Empty);

            var employees = JsonConvert.DeserializeObject<List<BlogRead>>(json);

            var articlesList = employees.Select(article => new BlogArticleViewModel()
            {
                Id = article.Id,
                Title = article.Title,
                DatePublished = article.DatePublished,
                PublisherId = article.PublisherId,
            });


            return View(new BlogArticlesListViewModel
            {
                BlogArticles = articlesList
                    .OrderBy(art => art.Id)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = employees.Count()
                }
            });
        }
    }
}
