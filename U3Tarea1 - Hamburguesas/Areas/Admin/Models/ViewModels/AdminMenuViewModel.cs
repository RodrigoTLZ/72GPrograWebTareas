namespace U3Tarea1___Hamburguesas.Areas.Admin.Models.ViewModels
{
    public class AdminMenuViewModel
    {
        public IEnumerable<ClasificacionModel> ListaClasificaciones { get; set; } = null!;
    }

    public class ClasificacionModel
    {
        public string Nombre { get; set; } = null!;
        public IEnumerable<MenuModel> ListaMenu { get; set; } = null!;
        
    }

    public class MenuModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public double Precio { get; set; }
        public double? PrecioPromocion { get; set; }
    }
}
