using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class PrecioPaqueteria
{
    public int IdPrecioPaqueteria { get; set; }

    public string? IdPaqueteria { get; set; }

    public decimal? CompraTotalPaqueteria { get; set; }

    public decimal? PrecioUnidadPaqueteria { get; set; }

    public int? StockTotalCompradoPaqueteria { get; set; }

    public DateTime? FechaCompra { get; set; }

    public DateTime? FechaInicioUso { get; set; }

    public DateTime? FechaFinUso { get; set; }

    public virtual Paqueteria? IdPaqueteriaNavigation { get; set; }
}
