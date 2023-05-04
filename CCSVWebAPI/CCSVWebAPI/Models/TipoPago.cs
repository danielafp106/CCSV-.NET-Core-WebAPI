using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CCSVWebAPI.Models;

public partial class TipoPago
{
    public string IdTipoPago { get; set; } = null!;

    public string? NombreTipoPago { get; set; }
    [JsonIgnore]
    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
