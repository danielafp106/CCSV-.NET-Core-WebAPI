using System;
using System.Collections.Generic;


namespace CCSVWebAPI.Models;

public partial class Corte
{
    public string IdCorte { get; set; } = null!;

    public DateTime? FechaInicioCorte { get; set; }

    public DateTime? FechaFinCorte { get; set; }

    public decimal? TotalVendido { get; set; }

    public decimal? TotalGanancias { get; set; }

    public int? CantidadVendido { get; set; }
}
