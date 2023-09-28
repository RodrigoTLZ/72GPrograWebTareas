using Microsoft.AspNetCore.Mvc;

namespace U1Tarea1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Perfil()
        {
            return View();
        }
    }
}
