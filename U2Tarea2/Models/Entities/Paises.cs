using System;
using System.Collections.Generic;

namespace U2Tarea2.Models.Entities;

public partial class Paises
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Razas> Razas { get; set; } = new List<Razas>();
}
