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
    public class ClienteController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public ClienteController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerClientes")]
        public IActionResult ObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                clientes = _dbContext.Clientes.Include(p=>p.DireccionesClientes).Include(p=>p.TelefonosClientes).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = clientes });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = clientes });
            }
        }

        [HttpGet]
        [Route("ObtenerCliente/{idCliente}")]
        public IActionResult ObtenerCliente(string idCliente)
        {
            Cliente cliente = _dbContext.Clientes.Find(idCliente);
            if (cliente == null)
            {
                return BadRequest("Ese cliente no existe.");
            }
            try
            {
                cliente = _dbContext.Clientes.Include(p => p.DireccionesClientes).Include(p => p.TelefonosClientes).Where(p => p.IdCliente == idCliente).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = cliente });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = cliente });
            }
        }

        [HttpPost]
        [Route("GuardarCliente")]
        public IActionResult GuardarCliente([FromBody] Cliente cliente)
        {
            try
            {
                string ID = GenerarID();
                cliente.IdCliente = ID;
                _dbContext.Clientes.Add(cliente);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarCliente")]
        public IActionResult EditarCliente([FromBody] Cliente clienteModificado)
        {
            Cliente clienteOriginal = _dbContext.Clientes.Find(clienteModificado.IdCliente);
            if (clienteOriginal == null)
            {
                return BadRequest("Ese cliente no existe.");
            }

            try
            {
                clienteOriginal.NombreCliente = clienteModificado.NombreCliente is null ? clienteOriginal.NombreCliente : clienteModificado.NombreCliente;

                _dbContext.Clientes.Update(clienteOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("VerificarIDCliente/{idCliente}")]
        public IActionResult VerificarIDCliente(string idCliente)
        {
            Cliente clienteOriginal = _dbContext.Clientes.Find(idCliente);
            if (clienteOriginal == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = false });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = true });
            }
        }

        [HttpDelete]
        [Route("EliminarCliente/{idCliente}")]
        public IActionResult EliminarCliente(string idCliente)
        {
            Cliente clienteEliminar = _dbContext.Clientes.Find(idCliente);
            if (clienteEliminar == null)
            {
                return BadRequest("Esa cliente no existe.");
            }

            try
            {
                _dbContext.Clientes.Remove(clienteEliminar);
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
            int indice = _dbContext.Clientes.Count() + 1;
            string ID = "CLT";
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
