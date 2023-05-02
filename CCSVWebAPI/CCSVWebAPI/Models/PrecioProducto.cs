using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CCSVWebAPI.Models;

public partial class PrecioProducto
{
    public int IdPrecioProducto { get; set; }

    public string? IdProducto { get; set; }

    public string? IdPedido { get; set; }

    public decimal? CompraTotalProducto { get; set; }

    public decimal? CompraUnidadProducto { get; set; }

    public decimal? Importacion { get; set; }

    public decimal? Paqueteria { get; set; }

    public decimal? TarifaEnvio { get; set; }

    public decimal? CompraUnidadPublico { get; set; }

    public decimal? Ganancia { get; set; }

    public decimal? PrecioPublico { get; set; }

    public int? StockTotalComprado { get; set; }

    public int? StockTotalRealTime { get; set; }

    public virtual ICollection<DetalleProductoModelo> DetalleProductosModelos { get; set; } = new List<DetalleProductoModelo>();
    [JsonIgnore]
    public virtual Pedido? Pedido { get; set; }
    [JsonIgnore]
    public virtual Producto? Producto { get; set; }
}
