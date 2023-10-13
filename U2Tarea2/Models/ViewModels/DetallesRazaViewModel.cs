using System.Security.Policy;

namespace U2Tarea2.Models.ViewModels
{
    public class DetallesRazaViewModel
    {
        public uint Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string OtrosNombres { get; set; } = null!;
        public string PaisOrigen { get;set; } = null!;
        public float PesoMin { get; set; }
        public float PesoMax { get; set; }
        public float AlturaMin { get; set; }
        public float AlturaMax { get; set; }
        public uint EsperanzaVida { get; set; }
        public uint NivelEnergia { get; set; }
        public uint FacilidadEntrenamiento { get; set; }
        public uint EjercicioObligatorio { get; set; }
        public uint AmistadDesconocidos { get; set; }
        public uint AmistadPerros { get; set; }
        public uint NecesidadCepillado { get; set; }
        public string Patas { get; set; } = null!;
        public string Cola { get; set; } = null!;
        public string Hocico { get; set; } = null!;
        public string Pelo { get; set; } = null!;
        public string Color { get; set; } = null!;

        public IEnumerable<PerroModel> ListadoPerrosRandom { get; set; } = null!;

    }
}
