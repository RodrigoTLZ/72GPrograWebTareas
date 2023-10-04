namespace U2Tarea1.Models.ViewModels
{
    public class IndexEspeciesViewModel
    {
        public int IdClase { get; set; }
        public string NombreClase { get; set; } = null!;
        public IEnumerable<EspecieModel> AnimalesEspecie { get; set; } = null!;
    }

    public class EspecieModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
