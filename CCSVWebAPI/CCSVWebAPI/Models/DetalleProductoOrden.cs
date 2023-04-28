using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class DetalleProductoOrden
{
    public int IdDetalleProductoOrden { get; set; }

    public string? IdOrden { get; set; }

    public int? IdDetalleProductoModelo { get; set; }

    public int? CantidadOrden { get; set; }

    public virtual DetalleProductoModelo? IdDetalleProductoModeloNavigation { get; set; }

    public virtual Orden? IdOrdenNavigation { get; set; }
}
