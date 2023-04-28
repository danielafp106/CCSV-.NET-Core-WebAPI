﻿using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class TipoPago
{
    public string IdTipoPago { get; set; } = null!;

    public string? NombreTipoPago { get; set; }

    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
