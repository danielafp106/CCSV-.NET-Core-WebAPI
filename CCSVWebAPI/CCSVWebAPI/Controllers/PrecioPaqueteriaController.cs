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
    public class PrecioPaqueteriaController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public PrecioPaqueteriaController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerPreciosPaqueteria")]
        public IActionResult ObtenerPreciosPaqueteria()
        {
            List<PrecioPaqueteria> pps = new List<PrecioPaqueteria>();
            try
            {
                pps = _dbContext.PreciosPaqueteria.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = pps });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = pps });
            }
        }

        [HttpGet]
        [Route("ObtenerPrecioPaqueteria/{idPrecioPaqueteria}")]
        public IActionResult ObtenerPrecioPaqueteria(int idPrecioPaqueteria)
        {
            PrecioPaqueteria pp = _dbContext.PreciosPaqueteria.Find(idPrecioPaqueteria);
            if (pp == null)
            {
                return BadRequest("Ese precio de paqueteria no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = pp });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = pp });
            }
        }

        [HttpPost]
        [Route("GuardarPrecioPaqueteria")]
        public IActionResult GuardarPrecioPaqueteria([FromBody] PrecioPaqueteria pp)
        {
            try
            {
                _dbContext.PreciosPaqueteria.Add(pp);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarPrecioPaqueteria")]
        public IActionResult EditarPrecioPaqueteria([FromBody] PrecioPaqueteria ppModificado)
        {
            PrecioPaqueteria ppOriginal = _dbContext.PreciosPaqueteria.Find(ppModificado.IdPrecioPaqueteria);
            if (ppOriginal == null)
            {
                return BadRequest("Ese pp no existe.");
            }

            try
            {
                ppOriginal.CompraTotalPaqueteria = ppModificado.CompraTotalPaqueteria is null ? ppOriginal.CompraTotalPaqueteria : ppModificado.CompraTotalPaqueteria;
                ppOriginal.PrecioUnidadPaqueteria = ppModificado.PrecioUnidadPaqueteria is null ? ppOriginal.PrecioUnidadPaqueteria : ppModificado.PrecioUnidadPaqueteria;
                ppOriginal.StockTotalCompradoPaqueteria = ppModificado.StockTotalCompradoPaqueteria is null ? ppOriginal.StockTotalCompradoPaqueteria : ppModificado.StockTotalCompradoPaqueteria;
                ppOriginal.FechaCompra = ppModificado.FechaCompra is null ? ppOriginal.FechaCompra : ppModificado.FechaCompra;
                ppOriginal.FechaInicioUso = ppModificado.FechaInicioUso is null ? ppOriginal.FechaInicioUso : ppModificado.FechaInicioUso;
                ppOriginal.FechaFinUso = ppModificado.FechaFinUso is null ? ppOriginal.FechaFinUso : ppModificado.FechaFinUso;
                ppOriginal.EsPaqueteriaRecurrente = ppModificado.EsPaqueteriaRecurrente is null ? ppOriginal.EsPaqueteriaRecurrente : ppModificado.EsPaqueteriaRecurrente;


                _dbContext.PreciosPaqueteria.Update(ppOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("VerificarIDPrecioPaqueteria/{idPrecioPaqueteria}")]
        public IActionResult VerificarIDPrecioPaqueteria(int idPrecioPaqueteria)
        {
            PrecioPaqueteria ppOriginal = _dbContext.PreciosPaqueteria.Find(idPrecioPaqueteria);
            if (ppOriginal == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = false });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = true });
            }
        }

        [HttpDelete]
        [Route("EliminarPrecioPaqueteria/{idPrecioPaqueteria}")]
        public IActionResult EliminarPrecioPaqueteria(int idPrecioPaqueteria)
        {
            PrecioPaqueteria ppEliminar = _dbContext.PreciosPaqueteria.Find(idPrecioPaqueteria);
            if (ppEliminar == null)
            {
                return BadRequest("Esa precio de paqueteria no existe.");
            }

            try
            {
                _dbContext.PreciosPaqueteria.Remove(ppEliminar);
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
