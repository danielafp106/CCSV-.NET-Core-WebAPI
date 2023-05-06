using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CCSVWebAPI.Models;

public partial class DireccionCliente
{
    public int IdDireccionCliente { get; set; }

    public string? IdCliente { get; set; }

    public string? MunicipioCliente { get; set; }

    public string? DepartamentoCliente { get; set; }

    public string? Direccion { get; set; }
    [JsonIgnore]
    public virtual Cliente? IdClienteNavigation { get; set; }
}
