using Microsoft.AspNetCore.Mvc;
using NorthwindMvcClient.ViewModels;
using System.Text.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Northwind.Services.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                BaseAddress = new Uri("https://localhost:7041/api/Employees/")
            };

            this._httpClient.DefaultRequestHeaders.Accept.Clear();
            this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index(int productPage = 1)
        {
            var json = await this._httpClient.GetStringAsync(string.Empty);

            var employees = JsonConvert.DeserializeObject<List<Employee>>(json);

            var employeesList = employees.Select(employee => new EmployeeViewModel()
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                Photo = new FormFile(new MemoryStream(employee.Photo), 78, employee.Photo.Length, "name", "fileName"),
            });


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
                    TotalItems = employees.Count()
                }
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var jsonEmployee = await this._httpClient.GetStringAsync($"{id}");
            var employee = JsonConvert.DeserializeObject<Employee>(jsonEmployee);

            var employeeView = new EmployeeViewModel()
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                Photo = new FormFile(new MemoryStream(employee.Photo), 78, employee.Photo.Length, "name", "fileName"),
            };

            return View(employeeView);
        }


        public async Task<IActionResult> Update(int id)
        {
            var jsonEmployee = await this._httpClient.GetStringAsync($"{id}");
            var employee = JsonConvert.DeserializeObject<Employee>(jsonEmployee);

            var jsonEmployees = await this._httpClient.GetStringAsync(string.Empty);
            var employees = JsonConvert.DeserializeObject<List<Employee>>(jsonEmployees);

            ViewBag.employees = new SelectList(employees, "Id", "LastName");

            var employeeView = new EmployeeViewModel()
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                HomePhone = employee.HomePhone,
                Extension = employee.Extension,
                Photo = new FormFile(new MemoryStream(employee.Photo), 78, employee.Photo.Length, "name", "fileName"),
                Notes = employee.Notes,
                ReportsTo = employee.ReportsTo,
                PhotoPath = employee.PhotoPath,
            };

            return View(employeeView);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeViewModel employee)
        {
            var imageData = new byte[(int)employee.Photo.Length + 77];

            using var binaryReader = new BinaryReader(employee.Photo.OpenReadStream());
            imageData.ToList().InsertRange(77, binaryReader.ReadBytes((int)employee.Photo.Length));
         
            var employeeServer = new Employee()
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                HomePhone = employee.HomePhone,
                Extension = employee.Extension,
                Photo = imageData,
                Notes = employee.Notes,
                ReportsTo = employee.ReportsTo,
                PhotoPath = employee.PhotoPath,
            };

            await this._httpClient.PutAsJsonAsync($"{employee.Id}", employeeServer);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var jsonEmployees = await this._httpClient.GetStringAsync(string.Empty);
            var employees = JsonConvert.DeserializeObject<List<Employee>>(jsonEmployees);

            ViewBag.employees = new SelectList(employees, "Id", "LastName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            var imageData = new byte[(int)employee.Photo.Length + 77];

            using var binaryReader = new BinaryReader(employee.Photo.OpenReadStream());
            imageData.ToList().InsertRange(77, binaryReader.ReadBytes((int)employee.Photo.Length));
            //var imageData = binaryReader.ReadBytes((int)employee.Photo.Length);

            var employeeServer = new Employee()
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                HomePhone = employee.HomePhone,
                Extension = employee.Extension,
                Photo = imageData,
                Notes = employee.Notes,
                ReportsTo = employee.ReportsTo,
                PhotoPath = employee.PhotoPath,
            };

            await this._httpClient.PostAsJsonAsync("api/Employees", employeeServer);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var json = await this._httpClient.GetStringAsync($"{id}");
            var employee = JsonConvert.DeserializeObject<Employee>(json);

            var employeeView = new EmployeeViewModel()
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                HomePhone = employee.HomePhone,
                Extension = employee.Extension,
                Photo = new FormFile(new MemoryStream(employee.Photo), 78, employee.Photo.Length, "name", "fileName"),
                Notes = employee.Notes,
                ReportsTo = employee.ReportsTo,
                PhotoPath = employee.PhotoPath,
            };

            return View(employeeView);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employee)
        {
            await this._httpClient.DeleteAsync($"{employee.Id}");

            return RedirectToAction("Index");
        }
    }
}