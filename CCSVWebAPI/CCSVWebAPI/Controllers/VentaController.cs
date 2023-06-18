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
    public class VentaController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public VentaController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerVentas")]
        public IActionResult ObtenerVentas()
        {
            List<Venta> ventaes = new List<Venta>();
            try
            {
                ventaes = _dbContext.Ventas.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = ventaes });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = ventaes });
            }
        }

        [HttpGet]
        [Route("ObtenerVenta/{idVenta}")]
        public IActionResult ObtenerVenta(string idVenta)
        {
            Venta venta = _dbContext.Ventas.Find(idVenta);
            if (venta == null)
            {
                return BadRequest("Ese direccion de cliente no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = venta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = venta });
            }
        }

        [HttpPost]
        [Route("GuardarVenta")]
        public IActionResult GuardarVenta([FromBody] Venta venta)
        {
            try
            {
                string ID = GenerarID();
                venta.IdVenta = ID;
                _dbContext.Ventas.Add(venta);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarVenta")]
        public IActionResult EditarVenta([FromBody] Venta ventaModificado)
        {
            Venta ventaOriginal = _dbContext.Ventas.Find(ventaModificado.IdVenta);
            if (ventaOriginal == null)
            {
                return BadRequest("Esa venta no existe.");
            }

            try
            {
                ventaOriginal.FechaVenta = ventaModificado.FechaVenta is null ? ventaOriginal.FechaVenta : ventaModificado.FechaVenta;
                ventaOriginal.CostoTotalVenta = ventaModificado.CostoTotalVenta is null ? ventaOriginal.CostoTotalVenta : ventaModificado.CostoTotalVenta;
                ventaOriginal.TarifaEnvio = ventaModificado.TarifaEnvio is null ? ventaOriginal.TarifaEnvio : ventaModificado.TarifaEnvio;
                ventaOriginal.CostoGuía = ventaModificado.CostoGuía is null ? ventaOriginal.CostoGuía : ventaModificado.CostoGuía;
                ventaOriginal.VentaTotalFinal = ventaModificado.VentaTotalFinal is null ? ventaOriginal.VentaTotalFinal : ventaModificado.VentaTotalFinal;
                ventaOriginal.Ganancia = ventaModificado.Ganancia is null ? ventaOriginal.Ganancia : ventaModificado.Ganancia;
                ventaOriginal.Descuento = ventaModificado.Descuento is null ? ventaOriginal.Descuento : ventaModificado.Descuento;

                _dbContext.Ventas.Update(ventaOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("VerificarIDVenta/{idVenta}")]
        public IActionResult VerificarIDVenta(string idVenta)
        {
            Venta ventaOriginal = _dbContext.Ventas.Find(idVenta);
            if (ventaOriginal == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = false });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = true });
            }
        }

        [HttpDelete]
        [Route("EliminarVenta/{idVenta}")]
        public IActionResult EliminarVenta(string idVenta)
        {
            Venta ventaEliminar = _dbContext.Ventas.Find(idVenta);
            if (ventaEliminar == null)
            {
                return BadRequest("Esa venta no existe.");
            }

            try
            {
                _dbContext.Ventas.Remove(ventaEliminar);
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
            int indice = _dbContext.Ventas.Count() + 1;
            string ID = "VTA";
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
