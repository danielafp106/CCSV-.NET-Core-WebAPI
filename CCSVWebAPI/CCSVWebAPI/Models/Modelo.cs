using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class Modelo
{
    public string IdModelo { get; set; } = null!;

    public string? IdMarca { get; set; }

    public string? NombreModelo { get; set; }

    public virtual ICollection<DetalleProductoModelo> DetalleProductosModelos { get; set; } = new List<DetalleProductoModelo>();

    public virtual Marca? IdMarcaNavigation { get; set; }
}
