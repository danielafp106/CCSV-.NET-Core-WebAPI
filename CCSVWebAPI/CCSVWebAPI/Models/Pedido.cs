﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    public virtual Proveedor? Proveedor { get; set; }
    public virtual ICollection<PrecioProducto> PreciosProductos { get; set; } = new List<PrecioProducto>();
}
