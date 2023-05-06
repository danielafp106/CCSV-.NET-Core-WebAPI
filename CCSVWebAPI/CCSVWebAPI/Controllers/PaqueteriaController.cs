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
    public class PaqueteriaController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public PaqueteriaController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerPaqueteria")]
        public IActionResult ObtenerPaqueteria()
        {
            List<Paqueteria> paqueterias = new List<Paqueteria>();
            try
            {
                paqueterias = _dbContext.Paqueteria.Include(p => p.PreciosPaqueteria).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = paqueterias });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = paqueterias });
            }
        }
        [HttpGet]
        [Route("ObtenerPaqueteria/{idPaqueteria}")]
        public IActionResult ObtenerPaqueteria(string idPaqueteria)
        {
            Paqueteria paqueterias = _dbContext.Paqueteria.Find(idPaqueteria);
            if (paqueterias == null)
            {
                return BadRequest("Esa paqueterias no existe.");
            }
            try
            {
                paqueterias = _dbContext.Paqueteria.Include(p => p.PreciosPaqueteria).Where(p => p.IdPaqueteria == idPaqueteria).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = paqueterias });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = paqueterias });
            }
        }

        [HttpPost]
        [Route("GuardarPaqueteria")]
        public IActionResult GuardarPaqueteria([FromBody] Paqueteria paqueteria)
        {
            try
            {
                string ID = GenerarID();
                paqueteria.IdPaqueteria = ID;
                _dbContext.Paqueteria.Add(paqueteria);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarPaqueteria")]
        public IActionResult EditarPaqueteria([FromBody] Paqueteria paqueteriaModificado)
        {
            Paqueteria paqueteriaOriginal = _dbContext.Paqueteria.Find(paqueteriaModificado.IdPaqueteria);
            if (paqueteriaOriginal == null)
            {
                return BadRequest("Esa paqueteria no existe.");
            }

            try
            {
                paqueteriaOriginal.NombrePaqueteria = paqueteriaModificado.NombrePaqueteria is null ? paqueteriaOriginal.NombrePaqueteria : paqueteriaModificado.NombrePaqueteria;
                paqueteriaOriginal.IdProveedor = paqueteriaModificado.IdProveedor is null ? paqueteriaOriginal.IdProveedor : paqueteriaModificado.IdProveedor;
                paqueteriaOriginal.UrlImagenPaqueteria = paqueteriaModificado.UrlImagenPaqueteria is null ? paqueteriaOriginal.UrlImagenPaqueteria : paqueteriaModificado.UrlImagenPaqueteria;

                _dbContext.Paqueteria.Update(paqueteriaOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarPaqueteria/{idPaqueteria}")]
        public IActionResult EliminarPaqueteria(string idPaqueteria)
        {
            Paqueteria paqueteriaEliminar = _dbContext.Paqueteria.Find(idPaqueteria);
            if (paqueteriaEliminar == null)
            {
                return BadRequest("Esa paqueteria no existe.");
            }

            try
            {
                _dbContext.Paqueteria.Remove(paqueteriaEliminar);
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
            int indice = _dbContext.Paqueteria.Count() + 1;
            string ID = "PQT";
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
