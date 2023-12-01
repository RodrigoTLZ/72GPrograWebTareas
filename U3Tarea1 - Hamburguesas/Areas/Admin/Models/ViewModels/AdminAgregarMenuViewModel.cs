using U3Tarea1___Hamburguesas.Models.Entities;

namespace U3Tarea1___Hamburguesas.Areas.Admin.Models.ViewModels
{
    public class AdminAgregarMenuViewModel
    {
        public IEnumerable<ClasificacionesModel>? Clasificaciones { get; set; } = null!;
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public double Precio { get; set; }
        public double? PrecioPromocion { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdClasificacion { get; set; }
        public IFormFile? Archivo {get;set;}
    }

    public class ClasificacionesModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
