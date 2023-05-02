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
    public class PrecioProductoController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public PrecioProductoController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerPreciosProductos")]
        public IActionResult ObtenerPreciosProductos()
        {
            List<PrecioProducto> preciosproductos = new List<PrecioProducto>();
            try
            {
                preciosproductos = _dbContext.PreciosProductos.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = preciosproductos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = preciosproductos });
            }
        }

        [HttpGet]
        [Route("ObtenerPrecioProducto/{idPrecioProducto:int}")]
        public IActionResult ObtenerPrecioProducto(int idPrecioProducto)
        {
            PrecioProducto precioproducto = _dbContext.PreciosProductos.Find(idPrecioProducto);
            if (precioproducto == null)
            {
                return BadRequest("Ese registro de precios no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = precioproducto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = precioproducto });
            }
        }

        [HttpPut]
        [Route("ActualizarStockRealTime")]
        public IActionResult ActualizarStockRealTime([FromBody] PrecioProducto ppModificado)
        {
            PrecioProducto ppOriginal = _dbContext.PreciosProductos.Find(ppModificado.IdPrecioProducto);
            if (ppOriginal == null)
            {
                return BadRequest("Ese registro de precios no existe.");
            }

            try
            {
                ppOriginal.StockTotalRealTime = ppModificado.StockTotalRealTime is null ? ppOriginal.StockTotalRealTime : ppModificado.StockTotalRealTime;

                _dbContext.PreciosProductos.Update(ppOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarPrecioProducto")]
        public IActionResult EditarPrecioProducto([FromBody] PrecioProducto ppModificado)
        {
            PrecioProducto ppOriginal = _dbContext.PreciosProductos.Find(ppModificado.IdPrecioProducto);
            if (ppOriginal == null)
            {
                return BadRequest("Ese registro de precios no existe.");
            }

            try
            {
                ppOriginal.CompraTotalProducto = ppModificado.CompraTotalProducto is null ? ppOriginal.CompraTotalProducto : ppModificado.CompraTotalProducto;
                ppOriginal.CompraUnidadProducto = ppModificado.CompraUnidadProducto is null ? ppOriginal.CompraUnidadProducto : ppModificado.CompraUnidadProducto;
                ppOriginal.CompraUnidadPublico = ppModificado.CompraUnidadPublico is null ? ppOriginal.CompraUnidadPublico : ppModificado.CompraUnidadPublico;
                ppOriginal.Ganancia = ppModificado.Ganancia is null ? ppOriginal.Ganancia : ppModificado.Ganancia;
                ppOriginal.Importacion = ppModificado.Importacion is null ? ppOriginal.Importacion : ppModificado.Importacion;
                ppOriginal.Paqueteria = ppModificado.Paqueteria is null ? ppOriginal.Paqueteria : ppModificado.Paqueteria;
                ppOriginal.TarifaEnvio = ppModificado.TarifaEnvio is null ? ppOriginal.TarifaEnvio : ppModificado.TarifaEnvio;
                ppOriginal.StockTotalComprado = ppModificado.StockTotalComprado is null ? ppOriginal.StockTotalComprado : ppModificado.StockTotalComprado;
                ppOriginal.StockTotalRealTime = ppModificado.StockTotalRealTime is null ? ppOriginal.StockTotalRealTime : ppModificado.StockTotalRealTime;

                _dbContext.PreciosProductos.Update(ppOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }


        [HttpPost]
        [Route("GuardarPrecioProducto")]
        public IActionResult GuardarPrecioProducto([FromBody] PrecioProducto precioproducto)
        {
            try
            {
                _dbContext.PreciosProductos.Add(precioproducto);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarPrecioProducto/{idPrecioProducto:int}")]
        public IActionResult EliminarPrecioProducto(string idPrecioProducto)
        {
            PrecioProducto precioproducto = _dbContext.PreciosProductos.Find(idPrecioProducto);
            if (precioproducto == null)
            {
                return BadRequest("Ese registro de precios no existe.");
            }

            try
            {
                _dbContext.PreciosProductos.Remove(precioproducto);
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
