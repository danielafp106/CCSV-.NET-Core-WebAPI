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
    public class EstadoOrdenController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public EstadoOrdenController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerEstadosOrdenes")]
        public IActionResult ObtenerEstadosOrdenes()
        {
            List<EstadoOrden> estadoordens = new List<EstadoOrden>();
            try
            {
                estadoordens = _dbContext.EstadosOrdenes.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = estadoordens });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = estadoordens });
            }
        }

        [HttpGet]
        [Route("ObtenerEstadoOrden/{idEstadoOrden}")]
        public IActionResult ObtenerEstadoOrden(string idEstadoOrden)
        {
            EstadoOrden estadoorden = _dbContext.EstadosOrdenes.Find(idEstadoOrden);
            if (estadoorden == null)
            {
                return BadRequest("Ese estado de orden no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = estadoorden });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = estadoorden });
            }
        }

        [HttpPost]
        [Route("GuardarEstadoOrden")]
        public IActionResult GuardarEstadoOrden([FromBody] EstadoOrden estadoorden)
        {
            try
            {
                _dbContext.EstadosOrdenes.Add(estadoorden);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarEstadoOrden")]
        public IActionResult EditarEstadoOrden([FromBody] EstadoOrden estadoordenModificado)
        {
            EstadoOrden estadoordenOriginal = _dbContext.EstadosOrdenes.Find(estadoordenModificado.IdEstadoOrden);
            if (estadoordenOriginal == null)
            {
                return BadRequest("Ese estado de orden no existe.");
            }

            try
            {
                estadoordenOriginal.NombreEstadoOrden = estadoordenModificado.NombreEstadoOrden is null ? estadoordenOriginal.NombreEstadoOrden : estadoordenModificado.NombreEstadoOrden;

                _dbContext.EstadosOrdenes.Update(estadoordenOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("VerificarIDEstadoOrden/{idEO}")]
        public IActionResult VerificarIDEstadoOrden(string idEO)
        {
            EstadoOrden estadoordenOriginal = _dbContext.EstadosOrdenes.Find(idEO);
            if (estadoordenOriginal == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = false });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = true });
            }
        }

        [HttpDelete]
        [Route("EliminarEstadoOrden/{idEstadoOrden}")]
        public IActionResult EliminarEstadoOrden(string idEstadoOrden)
        {
            EstadoOrden estadoordenEliminar = _dbContext.EstadosOrdenes.Find(idEstadoOrden);
            if (estadoordenEliminar == null)
            {
                return BadRequest("Esa estado de orden no existe.");
            }

            try
            {
                _dbContext.EstadosOrdenes.Remove(estadoordenEliminar);
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
