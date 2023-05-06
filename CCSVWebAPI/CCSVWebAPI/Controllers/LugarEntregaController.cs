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
    public class LugarEntregaController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public LugarEntregaController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerLugaresEntregas")]
        public IActionResult ObtenerLugaresEntregas()
        {
            List<LugarEntrega> lugarentregas = new List<LugarEntrega>();
            try
            {
                lugarentregas = _dbContext.LugaresEntregas.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = lugarentregas });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = lugarentregas });
            }
        }

        [HttpGet]
        [Route("ObtenerLugarEntrega/{idLugarEntrega}")]
        public IActionResult ObtenerLugarEntrega(string idLugarEntrega)
        {
            LugarEntrega lugarentrega = _dbContext.LugaresEntregas.Find(idLugarEntrega);
            if (lugarentrega == null)
            {
                return BadRequest("Ese lugar de entrega no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = lugarentrega });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = lugarentrega });
            }
        }

        [HttpPost]
        [Route("GuardarLugarEntrega")]
        public IActionResult GuardarLugarEntrega([FromBody] LugarEntrega lugarentrega)
        {
            try
            {
                _dbContext.LugaresEntregas.Add(lugarentrega);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarLugarEntrega")]
        public IActionResult EditarLugarEntrega([FromBody] LugarEntrega lugarentregaModificado)
        {
            LugarEntrega lugarentregaOriginal = _dbContext.LugaresEntregas.Find(lugarentregaModificado.IdLugarEntrega);
            if (lugarentregaOriginal == null)
            {
                return BadRequest("Ese lugar de entrega no existe.");
            }

            try
            {
                lugarentregaOriginal.NombreLugarEntrega = lugarentregaModificado.NombreLugarEntrega is null ? lugarentregaOriginal.NombreLugarEntrega : lugarentregaModificado.NombreLugarEntrega;
                lugarentregaOriginal.Direccion = lugarentregaModificado.Direccion is null ? lugarentregaOriginal.Direccion : lugarentregaModificado.Direccion;
                lugarentregaOriginal.MunicipioCliente = lugarentregaModificado.MunicipioCliente is null ? lugarentregaOriginal.MunicipioCliente : lugarentregaModificado.MunicipioCliente;
                lugarentregaOriginal.DepartamentoCliente = lugarentregaModificado.DepartamentoCliente is null ? lugarentregaOriginal.DepartamentoCliente : lugarentregaModificado.DepartamentoCliente;

                _dbContext.LugaresEntregas.Update(lugarentregaOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("VerificarIDLugarEntrega/{idLE}")]
        public IActionResult VerificarIDLugarEntrega(string idLE)
        {
            LugarEntrega lugarentregaOriginal = _dbContext.LugaresEntregas.Find(idLE);
            if (lugarentregaOriginal == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = false });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = true });
            }
        }

        [HttpDelete]
        [Route("EliminarLugarEntrega/{idLugarEntrega}")]
        public IActionResult EliminarLugarEntrega(string idLugarEntrega)
        {
            LugarEntrega lugarentregaEliminar = _dbContext.LugaresEntregas.Find(idLugarEntrega);
            if (lugarentregaEliminar == null)
            {
                return BadRequest("Esa lugar de entrega no existe.");
            }

            try
            {
                _dbContext.LugaresEntregas.Remove(lugarentregaEliminar);
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
