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
    public class ProductoDefectoPerdidaController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public ProductoDefectoPerdidaController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerProductosDefectosPerdidas")]
        public IActionResult ObtenerProductosDefectosPerdidas()
        {
            List<ProductoDefectoPerdida> pdps = new List<ProductoDefectoPerdida>();
            try
            {
                pdps = _dbContext.ProductosDefectosPerdidas.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = pdps });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = pdps });
            }
        }

        [HttpGet]
        [Route("ObtenerProductoDefectoPerdida/{idProductoDefectoPerdida:int}")]
        public IActionResult ObtenerProductoDefectoPerdida(int idProductoDefectoPerdida)
        {
            ProductoDefectoPerdida precioproducto = _dbContext.ProductosDefectosPerdidas.Find(idProductoDefectoPerdida);
            if (precioproducto == null)
            {
                return BadRequest("Ese registro de defecto/perdida no existe.");
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
        [Route("EditarProductoDefectoPerdida")]
        public IActionResult EditarProductoDefectoPerdida([FromBody] ProductoDefectoPerdida pdpModificado)
        {
            ProductoDefectoPerdida pdpOriginal = _dbContext.ProductosDefectosPerdidas.Find(pdpModificado.IdProductoDefectoPerdida);
            if (pdpOriginal == null)
            {
                return BadRequest("Ese registro de defecto/perdida no existe.");
            }

            try
            {
                pdpOriginal.IdDetalleProductoModelo = pdpModificado.IdDetalleProductoModelo is null ? pdpOriginal.IdDetalleProductoModelo : pdpModificado.IdDetalleProductoModelo;
                pdpOriginal.StockPerdido = pdpModificado.StockPerdido is null ? pdpOriginal.StockPerdido : pdpModificado.StockPerdido;
                pdpOriginal.TotalPerdido = pdpModificado.TotalPerdido is null ? pdpOriginal.TotalPerdido : pdpModificado.TotalPerdido;              

                _dbContext.ProductosDefectosPerdidas.Update(pdpOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }


        [HttpPost]
        [Route("GuardarProductoDefectoPerdida")]
        public IActionResult GuardarProductoDefectoPerdida([FromBody] ProductoDefectoPerdida precioproducto)
        {
            try
            {
                _dbContext.ProductosDefectosPerdidas.Add(precioproducto);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarProductoDefectoPerdida/{idProductoDefectoPerdida:int}")]
        public IActionResult EliminarProductoDefectoPerdida(string idProductoDefectoPerdida)
        {
            ProductoDefectoPerdida precioproducto = _dbContext.ProductosDefectosPerdidas.Find(idProductoDefectoPerdida);
            if (precioproducto == null)
            {
                return BadRequest("Ese registro de defecto/perdida no existe.");
            }

            try
            {
                _dbContext.ProductosDefectosPerdidas.Remove(precioproducto);
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
