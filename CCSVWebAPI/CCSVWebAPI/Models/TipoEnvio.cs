using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class TipoEnvio
{
    public string IdTipoEnvio { get; set; } = null!;

    public string? NombreTipoEnvio { get; set; }

    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
