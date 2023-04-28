using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class LugarEntrega
{
    public string IdLugarEntrega { get; set; } = null!;

    public string? NombreLugarEntrega { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
