using System;
using System.Collections.Generic;

namespace CCSVWebAPI.Models;

public partial class Producto
{
    public string IdProducto { get; set; } = null!;

    public string? NombreProducto { get; set; }

    public string? UrlProductoProveedor { get; set; }

    public string? UrlImagenProducto { get; set; }

    public virtual ICollection<PrecioProducto> PreciosProductos { get; set; } = new List<PrecioProducto>();
}
