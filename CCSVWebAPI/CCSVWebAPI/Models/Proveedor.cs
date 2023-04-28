using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class Proveedor
{
    public string IdProveedor { get; set; } = null!;

    public string? NombreProveedor { get; set; }

    public string? ContactoProveedor { get; set; }

    public string? Comentarios { get; set; }

    public virtual ICollection<Paqueteria> Paqueteria { get; set; } = new List<Paqueteria>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
