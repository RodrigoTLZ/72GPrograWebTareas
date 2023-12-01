using Microsoft.EntityFrameworkCore;
using U3Tarea1___Hamburguesas.Areas.Admin.Models.ViewModels;
using U3Tarea1___Hamburguesas.Models.Entities;

namespace U3Tarea1___Hamburguesas.Repositories
{
    public class MenuRepository:Repository<Menu>
    {
        public MenuRepository(NeatContext context): base(context)
        {
                
        }


        public override IEnumerable<Menu> GetAll()
        {
            return Context.Menu.Include(x => x.IdClasificacionNavigation).OrderBy(x=>x.Nombre);
        }

        public Menu? GetHamburgesaByNombre(string nombre)
        {
            return Context.Menu.Include(x => x.IdClasificacionNavigation).FirstOrDefault(x=>x.Nombre == nombre);
        }
       
        public IEnumerable<Menu> GetMenusEnPromocion()
        {
            return Context.Menu.Where(x => x.PrecioPromocion > 0  || x.PrecioPromocion != null).OrderBy(x => x.Nombre);
        }
    }
}
