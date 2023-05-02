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
    public class MarcaController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public MarcaController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerMarcas")]
        public IActionResult ObtenerMarcas()
        {
            List<Marca> marcas = new List<Marca>();
            try
            {
                marcas = _dbContext.Marcas.Include(p=>p.Modelos).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = marcas });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = marcas });
            }
        }

        [HttpGet]
        [Route("ObtenerMarca/{idMarca}")]
        public IActionResult ObtenerMarca(string idMarca)
        {
            Marca marca = _dbContext.Marcas.Find(idMarca);
            if (marca == null)
            {
                return BadRequest("Ese marca no existe.");
            }
            try
            {
                marca = _dbContext.Marcas.Include(p => p.Modelos).Where(p => p.IdMarca == idMarca).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = marca });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = marca });
            }
        }

        [HttpPost]
        [Route("GuardarMarca")]
        public IActionResult GuardarMarca([FromBody] Marca marca)
        {
            try
            {
                string ID = GenerarID();
                marca.IdMarca = ID;
                _dbContext.Marcas.Add(marca);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarMarca")]
        public IActionResult EditarMarca([FromBody] Marca marcaModificado)
        {
            Marca marcaOriginal = _dbContext.Marcas.Find(marcaModificado.IdMarca);
            if (marcaOriginal == null)
            {
                return BadRequest("Ese marca no existe.");
            }

            try
            {
                marcaOriginal.NombreMarca = marcaModificado.NombreMarca is null ? marcaOriginal.NombreMarca : marcaModificado.NombreMarca;       

                _dbContext.Marcas.Update(marcaOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarMarca/{idMarca}")]
        public IActionResult EliminarMarca(string idMarca)
        {
            Marca marcaEliminar = _dbContext.Marcas.Find(idMarca);
            if (marcaEliminar == null)
            {
                return BadRequest("Esa marca no existe.");
            }

            try
            {
                _dbContext.Marcas.Remove(marcaEliminar);
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
            int indice = _dbContext.Marcas.Count() + 1;
            string ID = "MCA";
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
