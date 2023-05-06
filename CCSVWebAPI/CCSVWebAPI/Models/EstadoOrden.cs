using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CCSVWebAPI.Models;

public partial class EstadoOrden
{
    public string IdEstadoOrden { get; set; } = null!;

    public string? NombreEstadoOrden { get; set; }
    [JsonIgnore]
    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
