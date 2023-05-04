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
    public class MedioVentaController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public MedioVentaController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerMedioVentas")]
        public IActionResult ObtenerMedioVentas()
        {
            List<MedioVenta> medioventas = new List<MedioVenta>();
            try
            {
                medioventas = _dbContext.MediosVentas.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = medioventas });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = medioventas });
            }
        }

        [HttpGet]
        [Route("ObtenerMedioVenta/{idMedioVenta}")]
        public IActionResult ObtenerMedioVenta(string idMedioVenta)
        {
            MedioVenta medioventa = _dbContext.MediosVentas.Find(idMedioVenta);
            if (medioventa == null)
            {
                return BadRequest("Ese medio de venta no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = medioventa });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = medioventa });
            }
        }

        [HttpPost]
        [Route("GuardarMedioVenta")]
        public IActionResult GuardarMedioVenta([FromBody] MedioVenta medioventa)
        {
            try
            {
                _dbContext.MediosVentas.Add(medioventa);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarMedioVenta")]
        public IActionResult EditarMedioVenta([FromBody] MedioVenta medioventaModificado)
        {
            MedioVenta medioventaOriginal = _dbContext.MediosVentas.Find(medioventaModificado.IdMedioVenta);
            if (medioventaOriginal == null)
            {
                return BadRequest("Ese medio de venta no existe.");
            }

            try
            {
                medioventaOriginal.NombreMedioVenta = medioventaModificado.NombreMedioVenta is null ? medioventaOriginal.NombreMedioVenta : medioventaModificado.NombreMedioVenta;

                _dbContext.MediosVentas.Update(medioventaOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("VerificarIDMedioVenta/{idMV}")]
        public IActionResult VerificarIDMedioVenta(string idMV)
        {
            MedioVenta medioventaOriginal = _dbContext.MediosVentas.Find(idMV);
            if (medioventaOriginal == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = false});
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = true });
            }
        }

        [HttpDelete]
        [Route("EliminarMedioVenta/{idMedioVenta}")]
        public IActionResult EliminarMedioVenta(string idMedioVenta)
        {
            MedioVenta medioventaEliminar = _dbContext.MediosVentas.Find(idMedioVenta);
            if (medioventaEliminar == null)
            {
                return BadRequest("Esa medio de venta no existe.");
            }

            try
            {
                _dbContext.MediosVentas.Remove(medioventaEliminar);
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
