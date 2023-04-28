using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class EstadoOrden
{
    public string IdEstadoOrden { get; set; } = null!;

    public string? NombreEstadoOrden { get; set; }

    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
