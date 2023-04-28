using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class MedioVenta
{
    public string IdMedioVenta { get; set; } = null!;

    public string? NombreMedioVenta { get; set; }

    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
