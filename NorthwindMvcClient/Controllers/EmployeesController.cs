using Microsoft.AspNetCore.Mvc;
using NorthwindMvcClient.ViewModels;
using System.Text.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace NorthwindMvcClient.Controllers
{
    public class EmployeesController : Controller
    {
        public const int PageSize = 5;

        private HttpClient _httpClient;

        public EmployeesController()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7041/")
            };

            this._httpClient.DefaultRequestHeaders.Accept.Clear();
            this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index(int productPage = 1)
        {
            var json = await this._httpClient.GetStringAsync("api/Employees");

            var employees = JsonConvert.DeserializeObject<List<Employee>>(json);

            var employeesList = employees?.Select(employee => new EmployeeViewModel()
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                Image = employee.Photo
            }).ToList();

            ViewData["Title"] = "Employees";

            return View(new EmployeesListViewModel
            {
                Employees = employeesList
                    .OrderBy(empl => empl.Id)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = employeesList.Count()
                }
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var json = await this._httpClient.GetStringAsync($"api/Employees/{id}");

            var employee = JsonConvert.DeserializeObject<Employee>(json);

            var employeeView = new EmployeeViewModel()
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                Image = employee.Photo
            };

            ViewData["Title"] = "Details";

            return View(employeeView);
        }

    }
}
