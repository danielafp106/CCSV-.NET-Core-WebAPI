using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class Cliente
{
    public string IdCliente { get; set; } = null!;

    public string? NombreCliente { get; set; }

    public virtual ICollection<DireccionCliente> DireccionesClientes { get; set; } = new List<DireccionCliente>();

    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();

    public virtual ICollection<TelefonosCliente> TelefonosClientes { get; set; } = new List<TelefonosCliente>();
}
