using Microsoft.AspNetCore.Mvc;

namespace NorthwindMvcClient.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
