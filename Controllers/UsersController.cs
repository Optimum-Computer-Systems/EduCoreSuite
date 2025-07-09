using Microsoft.AspNetCore.Mvc;

namespace EduCoreSuite.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
