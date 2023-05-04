using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CCSVWebAPI.Models;

public partial class ProductoDefectoPerdida
{
    public int IdProductoDefectoPerdida { get; set; }

    public int? IdDetalleProductoModelo { get; set; }

    public int? StockPerdido { get; set; }

    public decimal? TotalPerdido { get; set; }
    [JsonIgnore]
    public virtual DetalleProductoModelo? IdDetalleProductoModeloNavigation { get; set; }
}
