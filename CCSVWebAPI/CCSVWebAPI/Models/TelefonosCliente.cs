using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class TelefonosCliente
{
    public int IdTelefonoCliente { get; set; }

    public string? IdCliente { get; set; }

    public string? TelefonoCliente { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }
}
