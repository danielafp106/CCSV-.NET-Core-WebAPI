using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CCSVWebAPI.Models;

public partial class Proveedor
{
    public string IdProveedor { get; set; } = null!;

    public string? NombreProveedor { get; set; }

    public string? ContactoProveedor { get; set; }

    public string? Comentarios { get; set; }

    [JsonIgnore]
    public virtual ICollection<Paqueteria> Paqueteria { get; set; } = new List<Paqueteria>();
    [JsonIgnore]
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
