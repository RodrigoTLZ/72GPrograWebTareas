using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Protocol.Core.Types;
using U3Tarea1___Hamburguesas.Areas.Admin.Models.ViewModels;
using U3Tarea1___Hamburguesas.Models.Entities;
using U3Tarea1___Hamburguesas.Repositories;

namespace U3Tarea1___Hamburguesas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly Repository<Clasificacion> clasificacionrepository;
        private readonly MenuRepository menuRepository;

        public MenuController(Repository<Clasificacion> clasificacionrepository, MenuRepository menuRepository)
        {
            this.clasificacionrepository = clasificacionrepository;
            this.menuRepository = menuRepository;
        }
        public IActionResult Index()
        {
            AdminMenuViewModel vm = new AdminMenuViewModel()
            {
                ListaClasificaciones = menuRepository.GetAll().GroupBy(x => x.IdClasificacionNavigation).Select(x => new ClasificacionModel
                {
                    Nombre = x.Key.Nombre,
                    ListaMenu = x.Where(h => h.IdClasificacion == x.Key.Id).Select(x => new MenuModel
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Precio = x.Precio,
                        Descripcion = x.Descripción,
                        PrecioPromocion = x.PrecioPromocion
                    })
                })
            };
            return View(vm);
        }

        public IActionResult Agregar()
        {
            AdminAgregarMenuViewModel vm = new();
            vm.Clasificaciones = clasificacionrepository.GetAll().OrderBy(x => x.Nombre).Select(x => new ClasificacionesModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            });
            return View(vm);
        }


        [HttpPost]
        public IActionResult Agregar(AdminAgregarMenuViewModel vm)
        {

            if (string.IsNullOrWhiteSpace(vm.Nombre))
            {
                ModelState.AddModelError("", "Debe escribir un nombre.");
            }
            if (string.IsNullOrWhiteSpace(vm.Descripcion))
            {
                ModelState.AddModelError("", "Debe escribir ua descripción.");
            }
            if(vm.Precio <= 0)
            {
                ModelState.AddModelError("", "Debe escribir un precio mayor a 0.");
            }
            if (vm.Archivo != null)
            {
                if (vm.Archivo.ContentType != "image/png")
                {
                    ModelState.AddModelError("", "Solo se permiten imagenes PNG.");
                }

                if (vm.Archivo.Length > 500 * 1024)
                {
                    ModelState.AddModelError("", "Solo se permiten archivos no mayores a 500Kb");
                }
            }

            if (ModelState.IsValid)
            {
                var menu = new Menu()
                {
                    Id = vm.Id,
                    Nombre = vm.Nombre,
                    Descripción = vm.Descripcion,
                    IdClasificacion = vm.IdClasificacion,
                    Precio = vm.Precio
                };
                menuRepository.Insert(menu);
                if(vm.Archivo == null)
                {
                    System.IO.File.Copy("wwwroot/hamburguesas/burguer.png", $"wwwroot/hamburguesas/{menu.Id}.png");
                }
                else
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{menu.Id}.png");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index");

            }

            vm.Clasificaciones = clasificacionrepository.GetAll().OrderBy(x => x.Nombre).Select(x => new ClasificacionesModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            });
            return View(vm);
        }

        public IActionResult Eliminar(int id)
        {
            var menu = menuRepository.Get(id);
            if(menu == null)
            {
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        [HttpPost]
        public IActionResult Eliminar(Menu m)
        {
            var menu = menuRepository.Get(m.Id);
            if (menu == null)
            {
                return RedirectToAction("Index");
            }
            menuRepository.Delete(menu);

            var ruta = $"wwwroot/hamburguesas/{m.Id}.png";
            if (System.IO.File.Exists(ruta))
            {
                System.IO.File.Delete(ruta);
            }


            return RedirectToAction("Index");
        }


        public IActionResult Editar(int id)
        {
            var menu = menuRepository.Get(id);
            if (menu == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                AdminAgregarMenuViewModel vm = new()
                {
                    Id = menu.Id,
                    Nombre = menu.Nombre,
                    Descripcion = menu.Descripción,
                    IdClasificacion = menu.IdClasificacion,
                    Precio = menu.Precio
                };
                vm.Clasificaciones = clasificacionrepository.GetAll().OrderBy(x => x.Nombre).Select(x => new ClasificacionesModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                });

                return View(vm);

            }
        }


        [HttpPost]
        public IActionResult Editar(AdminAgregarMenuViewModel vm)
        {

            if (string.IsNullOrWhiteSpace(vm.Nombre))
            {
                ModelState.AddModelError("", "Debe escribir un nombre.");
            }
            if (string.IsNullOrWhiteSpace(vm.Descripcion))
            {
                ModelState.AddModelError("", "Debe escribir ua descripción.");
            }
            if (vm.Precio <= 0)
            {
                ModelState.AddModelError("", "Debe escribir un precio mayor a 0.");
            }
            if (vm.Archivo != null)
            {
                if (vm.Archivo.ContentType != "image/png")
                {
                    ModelState.AddModelError("", "Solo se permiten imagenes PNG.");
                }

                if (vm.Archivo.Length > 500 * 1024)
                {
                    ModelState.AddModelError("", "Solo se permiten archivos no mayores a 500Kb");
                }
            }

            if (ModelState.IsValid)
            {
                var menu = menuRepository.Get(vm.Id);
                if(menu != null)
                {
                    menu.Nombre = vm.Nombre;
                    menu.Descripción = vm.Descripcion;
                    menu.IdClasificacion = vm.IdClasificacion;
                    menu.Precio = vm.Precio;
                }
                else
                {
                    return RedirectToAction("Index");
                }
                menuRepository.Update(menu);
                if (vm.Archivo != null)
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{vm.Id}.png");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index");

            }

            vm.Clasificaciones = clasificacionrepository.GetAll().OrderBy(x => x.Nombre).Select(x => new ClasificacionesModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            });
            return View(vm);
        }


        public IActionResult AgregarPromocion(int id)
        {
            var menu = menuRepository.Get(id);
            if(menu == null)
            {
                return RedirectToAction("Index");
            }
            AdminAgregarPromocionViewModel vm = new()
            {
                Id = id,
                Nombre = menu.Nombre,
                PrecioPromocion = menu.PrecioPromocion,
                PrecioReal = menu.Precio
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult AgregarPromocion(AdminAgregarPromocionViewModel vm)
        {
            if(vm.PrecioPromocion == vm.PrecioReal)
            {
                ModelState.AddModelError("", "La promoción debe tener un descuento menor al actual.");
            }
            if(vm.PrecioPromocion <= 0)
            {
                ModelState.AddModelError("", "La promoción debe ser mayor a 0.");
            }

            if (ModelState.IsValid)
            {
                var menu = menuRepository.Get(vm.Id);
                if(menu == null)
                {
                    return RedirectToAction("Index");
                }
                menu.Nombre = vm.Nombre;
                menu.Precio = vm.PrecioReal;
                menu.PrecioPromocion = vm.PrecioPromocion;
                menuRepository.Update(menu);
                return RedirectToAction("Index");
            }
            

            return View(vm);
        }



        public IActionResult EliminarPromocion(int id)
        {
            var menu = menuRepository.Get(id);
            if (menu == null)
            {
                return RedirectToAction("Index");
            }
            AdminAgregarPromocionViewModel vm = new()
            {
                Id = id,
                Nombre = menu.Nombre,
                PrecioPromocion = menu.PrecioPromocion,
                PrecioReal = menu.Precio
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult EliminarPromocion(AdminAgregarPromocionViewModel vm)
        {
                var menu = menuRepository.Get(vm.Id);
                if (menu == null)
                {
                    return RedirectToAction("Index");
                }
                menu.Nombre = vm.Nombre;
                menu.Precio = vm.PrecioReal;
                menu.PrecioPromocion = null;
                menuRepository.Update(menu);
                return RedirectToAction("Index");
        }


    }
}
