using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Northwind.Services.Models;
using NorthwindMvcClient.ViewModels;
using System.Net.Http.Headers;

namespace NorthwindMvcClient.Controllers
{
    public class ProductsController : Controller
    {
        public readonly int PageSize;

        private HttpClient _httpClient;

        public ProductsController(IConfiguration configuration)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri($"https://localhost:{configuration["port"]}/api/")
            };

            PageSize = int.Parse(configuration["pageSize"]);

            this._httpClient.DefaultRequestHeaders.Accept.Clear();
            this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index(int productPage = 1)
        {
            var productsJson = await this._httpClient.GetStringAsync("products");
            var products = JsonConvert.DeserializeObject<List<Product>>(productsJson);

            var categoryesJson = await this._httpClient.GetStringAsync("categories");
            var categoryes = JsonConvert.DeserializeObject<List<ProductCategory>>(categoryesJson);

            var productsList = products.Select(product => new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                CategoryName = categoryes.Find(category => category.Id == product.CategoryId).Name,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            });

            return View(new ProductsListViewModel
            {
                Products = productsList
                    .OrderBy(prod => prod.Id)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = productsList.Count()
                }
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var productJson = await this._httpClient.GetStringAsync($"products/{id}");
            var product = JsonConvert.DeserializeObject<Product>(productJson);

            var categoryJson = await this._httpClient.GetStringAsync($"categories/{product.CategoryId}");
            var category = JsonConvert.DeserializeObject<ProductCategory>(categoryJson);

            var productView = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                CategoryName = category.Name,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            };

            return View(productView);
        }

        public async Task<IActionResult> Update(int id)
        {
            var productJson = await this._httpClient.GetStringAsync($"products/{id}");
            var product = JsonConvert.DeserializeObject<Product>(productJson);

            var categoryesJson = await this._httpClient.GetStringAsync($"categories");
            var categoryes = JsonConvert.DeserializeObject<List<ProductCategory>>(categoryesJson);

            ViewBag.categoryes = new SelectList(categoryes, "Id", "Name");

            var productView = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            };

            return View(productView);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductViewModel product)
        {
            var productServer = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            };

            await this._httpClient.PutAsJsonAsync($"products/{product.Id}", productServer);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var productJson = await this._httpClient.GetStringAsync($"products/{id}");
            var product = JsonConvert.DeserializeObject<Product>(productJson);

            var categoryJson = await this._httpClient.GetStringAsync($"categories/{product.CategoryId}");
            var category = JsonConvert.DeserializeObject<ProductCategory>(categoryJson);

            var productView = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                CategoryName = category.Name,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            };

            return View(productView);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductViewModel product)
        {
            await this._httpClient.DeleteAsync($"products/{product.Id}");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            var categoriesJson = await this._httpClient.GetStringAsync("categories");
            var categories = JsonConvert.DeserializeObject<List<ProductCategory>>(categoriesJson);

            ViewBag.categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            var productServer = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            };

            await this._httpClient.PostAsJsonAsync("products", productServer);

            return RedirectToAction("Index");
        }
    }
}
