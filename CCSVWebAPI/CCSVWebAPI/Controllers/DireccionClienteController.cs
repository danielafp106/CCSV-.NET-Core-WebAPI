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
    public class DireccionClienteController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public DireccionClienteController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerDireccionClientes")]
        public IActionResult ObtenerDireccionClientes()
        {
            List<DireccionCliente> direccionclientes = new List<DireccionCliente>();
            try
            {
                direccionclientes = _dbContext.DireccionesClientes.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = direccionclientes });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = direccionclientes });
            }
        }

        [HttpGet]
        [Route("ObtenerDireccionCliente/{idDireccionCliente}")]
        public IActionResult ObtenerDireccionCliente(string idDireccionCliente)
        {
            DireccionCliente direccioncliente = _dbContext.DireccionesClientes.Find(idDireccionCliente);
            if (direccioncliente == null)
            {
                return BadRequest("Ese direccion de cliente no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = direccioncliente });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = direccioncliente });
            }
        }

        [HttpPost]
        [Route("GuardarDireccionCliente")]
        public IActionResult GuardarDireccionCliente([FromBody] DireccionCliente direccioncliente)
        {
            try
            {
                _dbContext.DireccionesClientes.Add(direccioncliente);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarDireccionCliente")]
        public IActionResult EditarDireccionCliente([FromBody] DireccionCliente direccionclienteModificado)
        {
            DireccionCliente direccionclienteOriginal = _dbContext.DireccionesClientes.Find(direccionclienteModificado.IdDireccionCliente);
            if (direccionclienteOriginal == null)
            {
                return BadRequest("Ese direccioncliente no existe.");
            }

            try
            {
                direccionclienteOriginal.MunicipioCliente = direccionclienteModificado.MunicipioCliente is null ? direccionclienteOriginal.MunicipioCliente : direccionclienteModificado.MunicipioCliente;
                direccionclienteOriginal.DepartamentoCliente = direccionclienteModificado.DepartamentoCliente is null ? direccionclienteOriginal.DepartamentoCliente : direccionclienteModificado.DepartamentoCliente;
                direccionclienteOriginal.Direccion = direccionclienteModificado.Direccion is null ? direccionclienteOriginal.Direccion : direccionclienteModificado.Direccion;

                _dbContext.DireccionesClientes.Update(direccionclienteOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("VerificarIDDireccionCliente/{idDireccionCliente}")]
        public IActionResult VerificarIDDireccionCliente(string idDireccionCliente)
        {
            DireccionCliente direccionclienteOriginal = _dbContext.DireccionesClientes.Find(idDireccionCliente);
            if (direccionclienteOriginal == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = false });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = true });
            }
        }

        [HttpDelete]
        [Route("EliminarDireccionCliente/{idDireccionCliente}")]
        public IActionResult EliminarDireccionCliente(string idDireccionCliente)
        {
            DireccionCliente direccionclienteEliminar = _dbContext.DireccionesClientes.Find(idDireccionCliente);
            if (direccionclienteEliminar == null)
            {
                return BadRequest("Esa direccioncliente no existe.");
            }

            try
            {
                _dbContext.DireccionesClientes.Remove(direccionclienteEliminar);
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
