using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class Paqueteria
{
    public string IdPaqueteria { get; set; } = null!;

    public string? IdProveedor { get; set; }

    public string? NombrePaqueteria { get; set; }

    public string? UrlImagenPaqueteria { get; set; }

    public virtual Proveedor? Proveedor { get; set; }

    public virtual ICollection<PrecioPaqueteria> PreciosPaqueteria { get; set; } = new List<PrecioPaqueteria>();
}
