using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CCSVWebAPI.Models;

public partial class Modelo
{
    public string IdModelo { get; set; } = null!;

    public string? IdMarca { get; set; }

    public string? NombreModelo { get; set; }
    [JsonIgnore]
    public virtual ICollection<DetalleProductoModelo> DetalleProductosModelos { get; set; } = new List<DetalleProductoModelo>();
    [JsonIgnore]
    public virtual Marca? Marca { get; set; }
}
