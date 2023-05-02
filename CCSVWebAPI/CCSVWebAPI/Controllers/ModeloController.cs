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
    public class ModeloController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public ModeloController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerModelos")]
        public IActionResult ObtenerModelos()
        {
            List<Modelo> modelos = new List<Modelo>();
            try
            {
                modelos = _dbContext.Modelos.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = modelos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = modelos });
            }
        }

        [HttpGet]
        [Route("ObtenerModelo/{idModelo}")]
        public IActionResult ObtenerModelo(string idModelo)
        {
            Modelo modelo = _dbContext.Modelos.Find(idModelo);
            if (modelo == null)
            {
                return BadRequest("Ese modelo no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = modelo });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = modelo });
            }
        }

        [HttpPost]
        [Route("GuardarModelo")]
        public IActionResult GuardarModelo([FromBody] Modelo modelo)
        {
            try
            {
                string ID = GenerarID();
                modelo.IdModelo = ID;
                _dbContext.Modelos.Add(modelo);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarModelo")]
        public IActionResult EditarModelo([FromBody] Modelo modeloModificado)
        {
            Modelo modeloOriginal = _dbContext.Modelos.Find(modeloModificado.IdModelo);
            if (modeloOriginal == null)
            {
                return BadRequest("Ese modelo no existe.");
            }

            try
            {
                modeloOriginal.IdMarca = modeloModificado.IdMarca is null ? modeloOriginal.IdMarca : modeloModificado.IdMarca;
                modeloOriginal.NombreModelo = modeloModificado.NombreModelo is null ? modeloOriginal.NombreModelo : modeloModificado.NombreModelo;

                _dbContext.Modelos.Update(modeloOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarModelo/{idModelo}")]
        public IActionResult EliminarModelo(string idModelo)
        {
            Modelo modeloEliminar = _dbContext.Modelos.Find(idModelo);
            if (modeloEliminar == null)
            {
                return BadRequest("Ese modelo no existe.");
            }

            try
            {
                _dbContext.Modelos.Remove(modeloEliminar);
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
            int indice = _dbContext.Modelos.Count() + 1;
            string ID = "MDO";
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
