using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CCSVWebAPI.Models;

public partial class Venta
{
    public string IdVenta { get; set; } = null!;

    public string? IdOrden { get; set; }

    public DateTime? FechaVenta { get; set; }

    public decimal? CostoTotalVenta { get; set; }

    public decimal? TarifaEnvio { get; set; }

    public decimal? CostoGuía { get; set; }

    public decimal? VentaTotalFinal { get; set; }

    public decimal? Ganancia { get; set; }
    [JsonIgnore]
    public virtual Orden? IdOrdenNavigation { get; set; }
}
