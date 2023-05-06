using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CCSVWebAPI.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Cors;
using System.Text.RegularExpressions;

namespace CCSVWebAPI.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleProductoOrdenController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public DetalleProductoOrdenController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerDetalleProductoOrdens")]
        public IActionResult ObtenerDetalleProductoOrdens()
        {
            List<DetalleProductoOrden> detallepos = new List<DetalleProductoOrden>();
            try
            {
                detallepos = _dbContext.DetalleProductosOrdenes.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = detallepos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = detallepos });
            }
        }

        [HttpGet]
        [Route("ObtenerDetalleProductoOrden/{idDetalleProductoOrden}")]
        public IActionResult ObtenerDetalleProductoOrden(string idDetalleProductoOrden)
        {
            DetalleProductoOrden detallepo = _dbContext.DetalleProductosOrdenes.Find(idDetalleProductoOrden);
            if (detallepo == null)
            {
                return BadRequest("Esa detalle de producto no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = detallepo });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = detallepo });
            }
        }

        [HttpPost]
        [Route("GuardarDetalleProductoOrden")]
        public IActionResult GuardarDetalleProductoOrden([FromBody] DetalleProductoOrden detallepo)
        {
            try
            {
                _dbContext.DetalleProductosOrdenes.Add(detallepo);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarDetalleProductoOrden")]
        public IActionResult EditarDetalleProductoOrden([FromBody] DetalleProductoOrden detallepoModificado)
        {
            DetalleProductoOrden detallepoOriginal = _dbContext.DetalleProductosOrdenes.Find(detallepoModificado.IdDetalleProductoOrden);
            if (detallepoOriginal == null)
            {
                return BadRequest("Esa detalle de producto no existe.");
            }

            try
            {
                detallepoOriginal.IdDetalleProductoModelo = detallepoModificado.IdDetalleProductoModelo is null ? detallepoOriginal.IdDetalleProductoModelo : detallepoModificado.IdDetalleProductoModelo;
                detallepoOriginal.CantidadOrden = detallepoModificado.CantidadOrden is null ? detallepoOriginal.CantidadOrden : detallepoModificado.CantidadOrden;
                detallepoOriginal.TotalOrdenadoDetalle = detallepoModificado.TotalOrdenadoDetalle is null ? detallepoOriginal.TotalOrdenadoDetalle : detallepoModificado.TotalOrdenadoDetalle;
                detallepoOriginal.TotalGananciaDetalle = detallepoModificado.TotalGananciaDetalle is null ? detallepoOriginal.TotalGananciaDetalle : detallepoModificado.TotalGananciaDetalle;

                _dbContext.DetalleProductosOrdenes.Update(detallepoOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("VerificarIDDetalleProductoOrden/{idDetalleProductoOrden}")]
        public IActionResult VerificarIDDetalleProductoOrden(string idDetalleProductoOrden)
        {
            DetalleProductoOrden detallepoOriginal = _dbContext.DetalleProductosOrdenes.Find(idDetalleProductoOrden);
            if (detallepoOriginal == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = false });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = true });
            }
        }

        [HttpDelete]
        [Route("EliminarDetalleProductoOrden/{idDetalleProductoOrden}")]
        public IActionResult EliminarDetalleProductoOrden(string idDetalleProductoOrden)
        {
            DetalleProductoOrden detallepoEliminar = _dbContext.DetalleProductosOrdenes.Find(idDetalleProductoOrden);
            if (detallepoEliminar == null)
            {
                return BadRequest("Esa detalle de producto no existe.");
            }

            try
            {
                _dbContext.DetalleProductosOrdenes.Remove(detallepoEliminar);
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
