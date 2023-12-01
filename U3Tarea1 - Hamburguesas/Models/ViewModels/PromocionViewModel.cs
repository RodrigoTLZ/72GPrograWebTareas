namespace U3Tarea1___Hamburguesas.Models.ViewModels
{
    public class PromocionViewModel
    {
        public int Id { get; set; }
        public string MenuSiguiente { get; set; } = null!;
        public string MenuNombre { get; set; } = null!;
        public string MenuAnterior { get; set; } = null!;

        public string Descripcion { get; set; } = null!;
        public double Precio { get; set; }
        public double? PrecioPromocion { get; set;}
        

    }
}
