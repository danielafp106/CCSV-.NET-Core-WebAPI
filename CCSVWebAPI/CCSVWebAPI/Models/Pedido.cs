using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class Pedido
{
    public string IdPedido { get; set; } = null!;

    public string? IdProveedor { get; set; }

    public DateTime? FechaOrdenado { get; set; }

    public DateTime? FechaRecibido { get; set; }

    public decimal? TotalProductosPedido { get; set; }

    public decimal? TotalImportePedido { get; set; }

    public decimal? TotalPedido { get; set; }

    public int? StockPedido { get; set; }

    public virtual Proveedor? IdProveedorNavigation { get; set; }

    public virtual ICollection<PrecioProducto> PreciosProductos { get; set; } = new List<PrecioProducto>();
}
