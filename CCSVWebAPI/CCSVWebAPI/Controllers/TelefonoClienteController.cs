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
    public class TelefonoClienteController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public TelefonoClienteController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerTelefonoClientes")]
        public IActionResult ObtenerTelefonoClientes()
        {
            List<TelefonosCliente> telefonoclientes = new List<TelefonosCliente>();
            try
            {
                telefonoclientes = _dbContext.TelefonosClientes.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = telefonoclientes });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = telefonoclientes });
            }
        }

        [HttpGet]
        [Route("ObtenerTelefonoCliente/{idTelefonoCliente}")]
        public IActionResult ObtenerTelefonoCliente(string idTelefonoCliente)
        {
            TelefonosCliente telefonocliente = _dbContext.TelefonosClientes.Find(idTelefonoCliente);
            if (telefonocliente == null)
            {
                return BadRequest("Ese telefono de cliente no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = telefonocliente });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = telefonocliente });
            }
        }

        [HttpPost]
        [Route("GuardarTelefonoCliente")]
        public IActionResult GuardarTelefonoCliente([FromBody] TelefonosCliente telefonocliente)
        {
            try
            {
                _dbContext.TelefonosClientes.Add(telefonocliente);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarTelefonoCliente")]
        public IActionResult EditarTelefonoCliente([FromBody] TelefonosCliente telefonoclienteModificado)
        {
            TelefonosCliente telefonoclienteOriginal = _dbContext.TelefonosClientes.Find(telefonoclienteModificado.IdTelefonoCliente);
            if (telefonoclienteOriginal == null)
            {
                return BadRequest("Ese telefonocliente no existe.");
            }

            try
            {
                telefonoclienteOriginal.TelefonoCliente = telefonoclienteModificado.TelefonoCliente is null ? telefonoclienteOriginal.TelefonoCliente : telefonoclienteModificado.TelefonoCliente;

                _dbContext.TelefonosClientes.Update(telefonoclienteOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("VerificarIDTelefonoCliente/{idTelefonoCliente}")]
        public IActionResult VerificarIDTelefonoCliente(string idTelefonoCliente)
        {
            TelefonosCliente telefonoclienteOriginal = _dbContext.TelefonosClientes.Find(idTelefonoCliente);
            if (telefonoclienteOriginal == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = false });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = true });
            }
        }

        [HttpDelete]
        [Route("EliminarTelefonoCliente/{idTelefonoCliente}")]
        public IActionResult EliminarTelefonoCliente(string idTelefonoCliente)
        {
            TelefonosCliente telefonoclienteEliminar = _dbContext.TelefonosClientes.Find(idTelefonoCliente);
            if (telefonoclienteEliminar == null)
            {
                return BadRequest("Esa telefono de cliente no existe.");
            }

            try
            {
                _dbContext.TelefonosClientes.Remove(telefonoclienteEliminar);
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
