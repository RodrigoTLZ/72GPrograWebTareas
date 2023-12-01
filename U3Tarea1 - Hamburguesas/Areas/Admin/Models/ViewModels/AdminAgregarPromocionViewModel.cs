namespace U3Tarea1___Hamburguesas.Areas.Admin.Models.ViewModels
{
    public class AdminAgregarPromocionViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public double PrecioReal { get; set; } 
        public double? PrecioPromocion { get; set; }

    }
}
