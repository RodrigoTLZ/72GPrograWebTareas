using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using U2Tarea2.Models.Entities;
using U2Tarea2.Models.ViewModels;

namespace U2Tarea2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string Id)
        {
            PerrosContext context = new PerrosContext();
            IndexRazasViewModel vm = new();
            var ListaLetras = context.Razas.OrderBy(X => X.Nombre).Select(x => x.Nombre[0]).ToList();
            vm.Letras = ListaLetras.Distinct();
            if (Id == null)
            {
               
                vm.ListaRazas = context.Razas.Select(p => new PerroModel
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                }).OrderBy(x => x.Nombre);
               
            }
            else
            {
                vm.ListaRazas = context.Razas.Where(x => x.Nombre.StartsWith(Id)).Select(y => new PerroModel
                {
                    Id = y.Id,
                    Nombre = y.Nombre
                }).OrderBy(X=>X.Nombre);
            }
            return View(vm);
        }

        public IActionResult IndexRazasXPais()
        {
            PerrosContext context = new();
            //List<IndexRazasXPaisViewModel> vm = new();
            //Es lo mismo el var datos que pasar un viewmodel, simplemente si son varios viewmodel se debe hacer un list(como arriba) y al final convertirlo a .ToList();
            //LO MEJOR ES USAR VM CUANDO SOLAMENTE SEA 1 VIEWMODEL Y VAR DATOS CUANDO REGRESES VARIOS VIEWMODEL (IENUMERABLE)
            var datos = context.Paises.Include(x => x.Razas).OrderBy(x => x.Nombre).Select(x => new IndexRazasXPaisViewModel
            {
                NombrePais = x.Nombre ?? "No disponible",
                ListaRazasXPais = x.Razas.OrderBy(x=>x.Nombre).Select(p => new RazaModel
                {
                    Id = p.Id,
                    Nombre = p.Nombre ?? "No disponible"
                }) 
            });
            return View(datos);
        }

        public IActionResult DetallesRaza(string Id)
        {
            Id = Id.Replace("-", " ");
            PerrosContext context = new();
            var random = new Random();
            var datos = context.Razas.Where(x => x.Nombre == Id).Include(x => x.IdPaisNavigation).Include(x => x.Estadisticasraza).Include(x => x.Caracteristicasfisicas).FirstOrDefault();
            if(datos != null)
            {
                DetallesRazaViewModel vm = new DetallesRazaViewModel()
                {
                    Id = datos.Id,
                    AlturaMax = datos.AlturaMax,
                    AlturaMin = datos.AlturaMin,
                    AmistadDesconocidos = datos.Estadisticasraza?.AmistadDesconocidos?? 0,
                    AmistadPerros = datos.Estadisticasraza?.AmistadPerros?? 0,
                    Cola = datos.Caracteristicasfisicas?.Cola??"No disponible",
                    Color = datos.Caracteristicasfisicas?.Color ?? "No disponible",
                    NecesidadCepillado = datos.Estadisticasraza?.NecesidadCepillado ?? 0,
                    Descripcion = datos.Descripcion,
                    EjercicioObligatorio = datos.Estadisticasraza?.EjercicioObligatorio ?? 0,
                    EsperanzaVida = datos.EsperanzaVida,
                    FacilidadEntrenamiento = datos.Estadisticasraza?.FacilidadEntrenamiento ?? 0,
                    Hocico = datos.Caracteristicasfisicas?.Hocico ?? "No disponible",
                    NivelEnergia = datos.Estadisticasraza?.NivelEnergia ?? 0,
                    Nombre = datos.Nombre,
                    OtrosNombres = datos.OtrosNombres ?? "No disponible",
                    PaisOrigen = datos.IdPaisNavigation.Nombre??"No disponible",
                    Patas = datos.Caracteristicasfisicas?.Patas ?? "No disponible",
                    Pelo = datos.Caracteristicasfisicas?.Pelo ?? "No disponible",
                    PesoMax = datos.PesoMax,
                    PesoMin = datos.PesoMin,
                    ListadoPerrosRandom = context.Razas.Where(x=>x.Nombre != Id).ToList().Select(x=> new PerroModel
                    {
                        Id = x.Id,
                        Nombre = x.Nombre
                    }).OrderBy( x => random.Next()).Take(4)
                };
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index");
            }            

        }
    }
}
