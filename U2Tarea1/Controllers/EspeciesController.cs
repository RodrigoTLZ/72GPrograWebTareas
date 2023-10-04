using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using U2Tarea1.Models.Entities;
using U2Tarea1.Models.ViewModels;

namespace U2Tarea1.Controllers
{
    public class EspeciesController : Controller
    {
        public IActionResult Index(string Id)
        {
            AnimalesContext context = new();
            var clase = context.Clase.FirstOrDefault(x => x.Nombre == Id);
            if(clase != null)
            {
                var especies = context.Especies.OrderBy(x => x.Especie).Where(x => x.IdClase == clase.Id).Select(x => new EspecieModel
                {
                    Id = x.Id,
                    Nombre = x.Especie ?? ""
                });

                IndexEspeciesViewModel vm = new()
                {
                    IdClase = clase?.Id ?? 0,
                    NombreClase = Id ?? "",
                    AnimalesEspecie = especies
                };
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult DetallesEspecie(string Id)
        {
            AnimalesContext context = new();
            var animal = context.Especies.Include(x=>x.IdClaseNavigation).FirstOrDefault(x => x.Especie == Id);
            if (animal == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                DetallesViewModel vm = new()
                {
                    Id = animal.Id,
                    Clase = animal.IdClaseNavigation?.Nombre ?? "No disponible",
                    Habitat = animal.Habitat ?? "No disponible",
                    Nombre = animal.Especie,
                    Observaciones = animal.Observaciones ?? "No disponible",
                    Peso = animal.Peso,
                    Tamaño = animal.Tamaño
                };
                return View(vm);
            }
            
        }
    }
}
