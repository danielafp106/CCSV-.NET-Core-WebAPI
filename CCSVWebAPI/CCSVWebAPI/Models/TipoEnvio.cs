using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CCSVWebAPI.Models;

public partial class TipoEnvio
{
    public string IdTipoEnvio { get; set; } = null!;

    public string? NombreTipoEnvio { get; set; }
    [JsonIgnore]
    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
