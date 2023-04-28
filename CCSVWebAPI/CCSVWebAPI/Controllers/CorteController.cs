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
    public class CorteController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public CorteController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerCortes")]
        public IActionResult ObtenerCortes()
        {
            List<Corte> cortes = new List<Corte>();
            try
            {
                cortes = _dbContext.Cortes.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = cortes });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = cortes });
            }
        }

        [HttpGet]
        [Route("ObtenerCorte/{idCorte}")]
        public IActionResult ObtenerCorte(string idCorte)
        {
            Corte corte = _dbContext.Cortes.Find(idCorte);
            if (corte == null)
            {
                return BadRequest("Ese corte no existe.");
            }
            try
            {               
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = corte });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = corte });
            }
        }

        [HttpPost]
        [Route("GuardarCorte")]
        public IActionResult GuardarCorte([FromBody] Corte corte)
        {
            try
            {
                string ID = GenerarID();
                corte.IdCorte = ID;
                _dbContext.Cortes.Add(corte);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK"});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});
            }
        }

        [HttpPut]
        [Route("EditarCorte")]
        public IActionResult EditarCorte([FromBody] Corte corteModificado)
        {
            Corte corteOriginal = _dbContext.Cortes.Find(corteModificado.IdCorte);
            if (corteOriginal == null)
            {
                return BadRequest("Ese corte no existe.");
            }

            try
            {
                corteOriginal.TotalVendido = corteModificado.TotalVendido is null ? corteOriginal.TotalVendido : corteModificado.TotalVendido;
                corteOriginal.TotalGanancias = corteModificado.TotalGanancias is null ? corteOriginal.TotalGanancias : corteModificado.TotalGanancias;
                corteOriginal.FechaInicioCorte = corteModificado.FechaInicioCorte is null ? corteOriginal.FechaInicioCorte : corteModificado.FechaInicioCorte;
                corteOriginal.FechaFinCorte = corteModificado.FechaFinCorte is null ? corteOriginal.FechaFinCorte : corteModificado.FechaFinCorte;
                corteOriginal.CantidadVendido = corteModificado.CantidadVendido is null ? corteOriginal.CantidadVendido : corteModificado.CantidadVendido;


                _dbContext.Cortes.Update(corteOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarCorte/{idCorte}")]
        public IActionResult EliminarCorte(string idCorte)
        {
            Corte corteEliminar = _dbContext.Cortes.Find(idCorte);
            if (corteEliminar == null)
            {
                return BadRequest("Ese corte no existe.");
            }

            try
            {
                _dbContext.Cortes.Remove(corteEliminar);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        private string GenerarID()
        {
                int indice = _dbContext.Cortes.Count() + 1;
                string ID = "CTE";
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
