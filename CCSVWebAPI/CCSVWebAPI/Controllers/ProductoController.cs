using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CCSVWebAPI.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Cors;

namespace CCSVWebAPI.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public ProductoController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerProductos")]
        public IActionResult ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                productos = _dbContext.Productos.Include(p => p.PreciosProductos).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = productos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = productos });
            }
        }
        [HttpGet]
        [Route("ObtenerProducto/{idProducto}")]
        public IActionResult ObtenerProducto(string idProducto)
        {
            Producto productos = _dbContext.Productos.Find(idProducto);
            if (productos == null)
            {
                return BadRequest("Ese productos no existe.");
            }
            try
            {
                productos = _dbContext.Productos.Include(p=>p.PreciosProductos).Where(p=>p.IdProducto== idProducto).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = productos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = productos });
            }
        }

        [HttpPost]
        [Route("GuardarProducto")]
        public IActionResult GuardarProducto([FromBody] Producto producto)
        {
            try
            {
                string ID = GenerarID();
                producto.IdProducto = ID;
                _dbContext.Productos.Add(producto);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarProducto")]
        public IActionResult EditarProducto([FromBody] Producto productoModificado)
        {
            Producto productoOriginal = _dbContext.Productos.Find(productoModificado.IdProducto);
            if (productoOriginal == null)
            {
                return BadRequest("Ese producto no existe.");
            }

            try
            {
                productoOriginal.NombreProducto = productoModificado.NombreProducto is null ? productoOriginal.NombreProducto : productoModificado.NombreProducto;
                productoOriginal.UrlProductoProveedor = productoModificado.UrlProductoProveedor is null ? productoOriginal.UrlProductoProveedor : productoModificado.UrlProductoProveedor;
                productoOriginal.UrlImagenProducto = productoModificado.UrlImagenProducto is null ? productoOriginal.UrlImagenProducto : productoModificado.UrlImagenProducto;

                _dbContext.Productos.Update(productoOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarProducto/{idProducto}")]
        public IActionResult EliminarProducto(string idProducto)
        {
            Producto productoEliminar = _dbContext.Productos.Find(idProducto);
            if (productoEliminar == null)
            {
                return BadRequest("Ese producto no existe.");
            }

            try
            {
                _dbContext.Productos.Remove(productoEliminar);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        private string GenerarID()
        {
            int indice = _dbContext.Productos.Count() + 1;
            string ID = "PRO";
            if (indice >= 0 && indice < 10) ID += "000000" + indice;
            else if (indice >= 10 && indice < 100) ID += "00000" + indice;
            else if (indice >= 100 && indice < 1000) ID += "0000" + indice;
            else if (indice >= 1000 && indice < 10000) ID += "000" + indice;
            else if (indice >= 10000 && indice < 100000) ID += "00" + indice;
            else if (indice >= 100000 && indice < 1000000) ID += "0" + indice;
            else if (indice >= 1000000 && indice < 10000000) ID += indice;
            return ID;
        }
    }
}
