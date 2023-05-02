using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CCSVWebAPI.Models;

public partial class DetalleProductoModelo
{
    public int IdDetalleProductoModelo { get; set; }

    public int? IdPrecioProducto { get; set; }

    public string? IdModelo { get; set; }

    public int? StockProductoModelo { get; set; }
    public int? StockRealTimeProductoModelo { get; set; }
    [JsonIgnore]
    public virtual ICollection<DetalleProductoOrden> DetalleProductosOrdenes { get; set; } = new List<DetalleProductoOrden>();
    [JsonIgnore]
    public virtual Modelo? Modelo { get; set; }
    [JsonIgnore]
    public virtual PrecioProducto? PrecioProducto { get; set; }
    [JsonIgnore]
    public virtual ICollection<ProductoDefectoPerdida> ProductosDefectosPerdida { get; set; } = new List<ProductoDefectoPerdida>();
}
