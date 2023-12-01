using Microsoft.AspNetCore.Mvc;
using U3Tarea1___Hamburguesas.Models.Entities;
using U3Tarea1___Hamburguesas.Models.ViewModels;
using U3Tarea1___Hamburguesas.Repositories;

namespace U3Tarea1___Hamburguesas.Controllers
{
    public class HomeController : Controller
    {
        public MenuRepository menuRepository;

        public Repository<Menu> Repository { get; }

        public HomeController(Repository<Menu> repository, MenuRepository menuRepository)
        {
            Repository = repository;
            this.menuRepository = menuRepository;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Promociones(string id)
        {
            var menuspromociones = menuRepository.GetMenusEnPromocion().Select(x=> new
            {
                Nombre = x.Nombre
            }).ToArray();
            if(menuspromociones != null)
            {
                if(id == null)
                {
                    id = menuspromociones[0].Nombre;
                }
                else
                {
                    id = id.Replace("-", " ");
                }
                    var menu = menuRepository.GetHamburgesaByNombre(id);
                    int indice = Array.FindIndex(menuspromociones, x => x.Nombre == id);
                    PromocionViewModel vm = new()
                    {
                        Id = menu?.Id ?? 0,
                        MenuNombre = id,
                        Precio = menu?.Precio ?? 0,
                        PrecioPromocion = menu?.PrecioPromocion ?? 0,
                        Descripcion = menu?.Descripción ?? "Sin descripción.",
                        MenuAnterior = menuspromociones[(indice -1 + menuspromociones.Length) % menuspromociones.Length].Nombre,
                        MenuSiguiente = menuspromociones[(indice + 1) % menuspromociones.Length].Nombre
                    };
                    return View(vm);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Menu(string id)
        {

            MenuViewModel vm = new();
            if(id == null)
            {

                vm.HamburguesaSeleccionada = menuRepository.GetAll().First();
                vm.ListaClasificaciones = menuRepository.GetAll().GroupBy(x => x.IdClasificacionNavigation).Select(x => new ClasificacionModel
                {
                    NombreClasificacion = x.Key.Nombre,
                    ListaMenu = x.Where(h => h.IdClasificacion == x.Key.Id).Select(x => new MenuModel
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Precio = x.Precio
                    })
                });
            }
            else
            {
                id = id.Replace("-", " ");
                vm.HamburguesaSeleccionada = menuRepository.GetHamburgesaByNombre(id);
                vm.ListaClasificaciones = menuRepository.GetAll().GroupBy(x => x.IdClasificacionNavigation).Select(x => new ClasificacionModel
                {
                    NombreClasificacion = x.Key.Nombre,
                    ListaMenu = x.Where(h => h.IdClasificacion == x.Key.Id).Select(x => new MenuModel
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Precio = x.Precio
                    })
                });
            }

            return View(vm);
        }


    }
}
