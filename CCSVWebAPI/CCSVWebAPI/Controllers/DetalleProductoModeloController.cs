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
    public class DetalleProductoModeloController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public DetalleProductoModeloController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerDetalleProductosModelos")]
        public IActionResult ObtenerDetalleProductosModelos()
        {
            List<DetalleProductoModelo> detallepms = new List<DetalleProductoModelo>();
            try
            {
                detallepms = _dbContext.DetalleProductosModelos.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = detallepms });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = detallepms });
            }
        }

        [HttpGet]
        [Route("ObtenerDetalleProductoModelo/{idDetalleProductoModelo:int}")]
        public IActionResult ObtenerDetalleProductoModelo(int idDetalleProductoModelo)
        {
            DetalleProductoModelo detallepm = _dbContext.DetalleProductosModelos.Find(idDetalleProductoModelo);
            if (detallepm == null)
            {
                return BadRequest("Ese detallepm no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = detallepm });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = detallepm });
            }
        }

        [HttpPost]
        [Route("GuardarDetalleProductoModelo")]
        public IActionResult GuardarDetalleProductoModelo([FromBody] DetalleProductoModelo detallepm)
        {
            try
            {
                _dbContext.DetalleProductosModelos.Add(detallepm);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarDetalleProductoModelo")]
        public IActionResult EditarDetalleProductoModelo([FromBody] DetalleProductoModelo detallepmModificado)
        {
            DetalleProductoModelo detallepmOriginal = _dbContext.DetalleProductosModelos.Find(detallepmModificado.IdDetalleProductoModelo);
            if (detallepmOriginal == null)
            {
                return BadRequest("Ese detallepm no existe.");
            }

            try
            {
                detallepmOriginal.IdPrecioProducto = detallepmModificado.IdPrecioProducto is null ? detallepmOriginal.IdPrecioProducto : detallepmModificado.IdPrecioProducto;
                detallepmOriginal.IdModelo = detallepmModificado.IdModelo is null ? detallepmOriginal.IdModelo : detallepmModificado.IdModelo;
                detallepmOriginal.StockProductoModelo = detallepmModificado.StockProductoModelo is null ? detallepmOriginal.StockProductoModelo : detallepmModificado.StockProductoModelo;
                detallepmOriginal.StockRealTimeProductoModelo = detallepmModificado.StockRealTimeProductoModelo is null ? detallepmOriginal.StockRealTimeProductoModelo : detallepmModificado.StockRealTimeProductoModelo;

                _dbContext.DetalleProductosModelos.Update(detallepmOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarDetalleProductoModelo/{idDetalleProductoModelo}")]
        public IActionResult EliminarDetalleProductoModelo(string idDetalleProductoModelo)
        {
            DetalleProductoModelo detallepmEliminar = _dbContext.DetalleProductosModelos.Find(idDetalleProductoModelo);
            if (detallepmEliminar == null)
            {
                return BadRequest("Ese detallepm no existe.");
            }

            try
            {
                _dbContext.DetalleProductosModelos.Remove(detallepmEliminar);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }
    }
}
