namespace U2Tarea2.Models.ViewModels
{
    public class IndexRazasViewModel
    {
        public IEnumerable<char> Letras { get; set; } = null!;
        public IEnumerable<PerroModel> ListaRazas { get; set; } = null!;
    }

    public class PerroModel
    {
        public uint Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
