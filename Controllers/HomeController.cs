using Microsoft.AspNetCore.Mvc;

namespace EduCoreSuite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}