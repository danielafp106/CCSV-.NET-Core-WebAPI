using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class Marca
{
    public string IdMarca { get; set; } = null!;

    public string? NombreMarca { get; set; }

    public virtual ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();
}
