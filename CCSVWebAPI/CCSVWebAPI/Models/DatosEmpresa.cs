using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class DatosEmpresa
{
    public string? NombreEmpresa { get; set; }

    public string? UrlLogoEmpresa { get; set; }

    public string? ColorPrincipal { get; set; }

    public string? ColorSecundario { get; set; }

    public string? ColorTerciario { get; set; }
}
