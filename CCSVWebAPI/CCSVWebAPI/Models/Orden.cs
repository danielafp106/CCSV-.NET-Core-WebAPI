using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class Orden
{
    public string IdOrden { get; set; } = null!;

    public string? IdCliente { get; set; }

    public string? IdMedioVenta { get; set; }

    public string? IdEstadoOrden { get; set; }

    public string? IdTipoEnvio { get; set; }

    public string? IdLugarEntrega { get; set; }

    public string? IdTipoPago { get; set; }

    public DateTime? FechaOrden { get; set; }

    public string? HoraEntregaOrden { get; set; }

    public string? ComentariosOrden { get; set; }

    public virtual ICollection<DetalleProductoOrden> DetalleProductosOrdenes { get; set; } = new List<DetalleProductoOrden>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual EstadoOrden? IdEstadoOrdenNavigation { get; set; }

    public virtual LugarEntrega? IdLugarEntregaNavigation { get; set; }

    public virtual MedioVenta? IdMedioVentaNavigation { get; set; }

    public virtual TipoEnvio? IdTipoEnvioNavigation { get; set; }

    public virtual TipoPago? IdTipoPagoNavigation { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
