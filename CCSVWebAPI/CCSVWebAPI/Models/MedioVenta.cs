using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CCSVWebAPI.Models;

public partial class MedioVenta
{
    public string IdMedioVenta { get; set; } = null!;

    public string? NombreMedioVenta { get; set; }
    [JsonIgnore]
    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
