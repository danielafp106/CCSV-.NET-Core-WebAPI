using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CCSVWebAPI.Models;

public partial class DetalleProductoOrden
{
    public int IdDetalleProductoOrden { get; set; }

    public string? IdOrden { get; set; }

    public int? IdDetalleProductoModelo { get; set; }

    public int? CantidadOrden { get; set; }

    public decimal? TotalGananciaDetalle { get; set; }

    public decimal? TotalOrdenadoDetalle { get; set; }

    public virtual DetalleProductoModelo? IdDetalleProductoModeloNavigation { get; set; }

    [JsonIgnore]
    public virtual Orden? IdOrdenNavigation { get; set; }
}
