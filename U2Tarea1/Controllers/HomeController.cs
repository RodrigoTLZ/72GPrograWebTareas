using Microsoft.AspNetCore.Mvc;
using U2Tarea1.Models.ViewModels;
using U2Tarea1.Models.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace U2Tarea1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            AnimalesContext context = new();
            IndexViewModel vm = new();
            var datos = context.Clase.OrderBy(x => x.Nombre);
            vm.Clases = datos.Select(x => new ClaseModel
            {
                Id = x.Id,
                Nombre = x.Nombre ?? "No disponible",
                Descripcion = x.Descripcion ?? "No disponible"
            });

            return View(vm);
        }
    }
}
