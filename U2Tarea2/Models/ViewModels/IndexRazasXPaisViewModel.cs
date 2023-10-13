namespace U2Tarea2.Models.ViewModels
{
    public class IndexRazasXPaisViewModel
    {
        public string NombrePais { get; set; } = null!;
        public IEnumerable<RazaModel> ListaRazasXPais { get; set; } = null!;
    }

    public class RazaModel
    {
        public uint Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
