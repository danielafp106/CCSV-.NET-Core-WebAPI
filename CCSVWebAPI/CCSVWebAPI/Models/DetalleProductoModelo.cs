using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class DetalleProductoModelo
{
    public int IdDetalleProductoModelo { get; set; }

    public int? IdPrecioProducto { get; set; }

    public string? IdModelo { get; set; }

    public int? StockProductoModelo { get; set; }

    public virtual ICollection<DetalleProductoOrden> DetalleProductosOrdenes { get; set; } = new List<DetalleProductoOrden>();

    public virtual Modelo? IdModeloNavigation { get; set; }

    public virtual PrecioProducto? IdPrecioProductoNavigation { get; set; }

    public virtual ICollection<ProductoDefectoPerdida> ProductosDefectosPerdida { get; set; } = new List<ProductoDefectoPerdida>();
}
