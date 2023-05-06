using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CCSVWebAPI.Models;

public partial class LugarEntrega
{
    public string IdLugarEntrega { get; set; } = null!;

    public string? NombreLugarEntrega { get; set; }

    public string? MunicipioCliente { get; set; }

    public string? DepartamentoCliente { get; set; }

    public string? Direccion { get; set; }
    [JsonIgnore]
    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
