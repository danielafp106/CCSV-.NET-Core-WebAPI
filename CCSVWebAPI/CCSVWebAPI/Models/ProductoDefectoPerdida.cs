using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class ProductoDefectoPerdida
{
    public int IdProductoDefectoPerdida { get; set; }

    public int? IdDetalleProductoModelo { get; set; }

    public int? StockPerdido { get; set; }

    public decimal? TotalPerdido { get; set; }

    public virtual DetalleProductoModelo? IdDetalleProductoModeloNavigation { get; set; }
}
