using U3Tarea1___Hamburguesas.Models.Entities;

namespace U3Tarea1___Hamburguesas.Models.ViewModels
{
    public class MenuViewModel
    {
        public Menu HamburguesaSeleccionada { get; set; } = null!;
        public IEnumerable<ClasificacionModel> ListaClasificaciones { get; set; } = null!;

    }

    public class ClasificacionModel
    {
        public string NombreClasificacion { get; set; } = null!;
        public IEnumerable<MenuModel> ListaMenu { get; set; } = null!;

    }

    public class MenuModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public double Precio { get; set; }
    }
}
