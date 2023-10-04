namespace U2Tarea1.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<ClaseModel> Clases { get; set; } = null!;
    }

    public class ClaseModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }

}
