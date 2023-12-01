using Microsoft.AspNetCore.Mvc;

namespace U3Tarea1___Hamburguesas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
