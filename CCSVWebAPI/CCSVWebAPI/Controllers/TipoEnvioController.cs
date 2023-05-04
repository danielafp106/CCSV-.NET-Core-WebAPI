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
    public class TipoEnvioController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public TipoEnvioController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerTiposEnvios")]
        public IActionResult ObtenerTiposEnvios()
        {
            List<TipoEnvio> tipoenvios = new List<TipoEnvio>();
            try
            {
                tipoenvios = _dbContext.TiposEnvios.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = tipoenvios });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = tipoenvios });
            }
        }

        [HttpGet]
        [Route("ObtenerTipoEnvio/{idTipoEnvio}")]
        public IActionResult ObtenerTipoEnvio(string idTipoEnvio)
        {
            TipoEnvio tipoenvio = _dbContext.TiposEnvios.Find(idTipoEnvio);
            if (tipoenvio == null)
            {
                return BadRequest("Ese tipo de envio no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = tipoenvio });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = tipoenvio });
            }
        }

        [HttpPost]
        [Route("GuardarTipoEnvio")]
        public IActionResult GuardarTipoEnvio([FromBody] TipoEnvio tipoenvio)
        {
            try
            {
                _dbContext.TiposEnvios.Add(tipoenvio);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarTipoEnvio")]
        public IActionResult EditarTipoEnvio([FromBody] TipoEnvio tipoenvioModificado)
        {
            TipoEnvio tipoenvioOriginal = _dbContext.TiposEnvios.Find(tipoenvioModificado.IdTipoEnvio);
            if (tipoenvioOriginal == null)
            {
                return BadRequest("Ese tipo de envio no existe.");
            }

            try
            {
                tipoenvioOriginal.NombreTipoEnvio = tipoenvioModificado.NombreTipoEnvio is null ? tipoenvioOriginal.NombreTipoEnvio : tipoenvioModificado.NombreTipoEnvio;

                _dbContext.TiposEnvios.Update(tipoenvioOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("VerificarIDTipoEnvio/{idTE}")]
        public IActionResult VerificarIDTipoEnvio(string idTE)
        {
            TipoEnvio tipoenvioOriginal = _dbContext.TiposEnvios.Find(idTE);
            if (tipoenvioOriginal == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = false });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = true });
            }
        }

        [HttpDelete]
        [Route("EliminarTipoEnvio/{idTipoEnvio}")]
        public IActionResult EliminarTipoEnvio(string idTipoEnvio)
        {
            TipoEnvio tipoenvioEliminar = _dbContext.TiposEnvios.Find(idTipoEnvio);
            if (tipoenvioEliminar == null)
            {
                return BadRequest("Esa tipo de envio no existe.");
            }

            try
            {
                _dbContext.TiposEnvios.Remove(tipoenvioEliminar);
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
