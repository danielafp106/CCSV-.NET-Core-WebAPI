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
    public class OrdenController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public OrdenController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerOrdenes")]
        public IActionResult ObtenerOrdens()
        {
            List<Orden> ordenes = new List<Orden>();
            try
            {
                ordenes = _dbContext.Ordenes
                    .Include(p => p.EstadoOrden)
                    .Include(p => p.MedioVenta)
                    .Include(p => p.TipoEnvio)
                    .Include(p => p.LugarEntrega)
                    .Include(p => p.TipoPago)
                    .Include(p => p.Cliente)
                    .Include(p => p.Venta)
                    .Include(p => p.DetalleProductosOrdenes)
                    .ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = ordenes });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = ordenes });
            }
        }

        [HttpGet]
        [Route("ObtenerOrden/{idOrden}")]
        public IActionResult ObtenerOrden(string idOrden)
        {
            Orden orden = _dbContext.Ordenes.Find(idOrden);
            if (orden == null)
            {
                return BadRequest("Ese direccion de cliente no existe.");
            }
            try
            {
                orden = _dbContext.Ordenes
                    .Include(p => p.EstadoOrden)
                    .Include(p => p.MedioVenta)
                    .Include(p => p.TipoEnvio)
                    .Include(p => p.LugarEntrega)
                    .Include(p => p.TipoPago)
                    .Include(p => p.Cliente)
                    .Include(p => p.Venta)
                    .Include(p => p.DetalleProductosOrdenes)
                    .Where(p => p.IdOrden == idOrden).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = orden });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = orden });
            }
        }

        [HttpPost]
        [Route("GuardarOrden")]
        public IActionResult GuardarOrden([FromBody] Orden orden)
        {
            try
            {
                string ID = GenerarID();
                orden.IdOrden = ID;
                _dbContext.Ordenes.Add(orden);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarOrden")]
        public IActionResult EditarOrden([FromBody] Orden ordenModificado)
        {
            Orden ordenOriginal = _dbContext.Ordenes.Find(ordenModificado.IdOrden);
            if (ordenOriginal == null)
            {
                return BadRequest("Esa orden no existe.");
            }

            try
            {
                ordenOriginal.IdCliente = ordenModificado.IdCliente is null ? ordenOriginal.IdCliente : ordenModificado.IdCliente;
                ordenOriginal.IdMedioVenta = ordenModificado.IdMedioVenta is null ? ordenOriginal.IdMedioVenta : ordenModificado.IdMedioVenta;
                ordenOriginal.IdEstadoOrden = ordenModificado.IdEstadoOrden is null ? ordenOriginal.IdEstadoOrden : ordenModificado.IdEstadoOrden;
                ordenOriginal.IdTipoEnvio = ordenModificado.IdTipoEnvio is null ? ordenOriginal.IdTipoEnvio : ordenModificado.IdTipoEnvio;
                ordenOriginal.IdLugarEntrega = ordenModificado.IdLugarEntrega is null ? ordenOriginal.IdLugarEntrega : ordenModificado.IdLugarEntrega;
                ordenOriginal.IdTipoPago = ordenModificado.IdTipoPago is null ? ordenOriginal.IdTipoPago : ordenModificado.IdTipoPago;
                ordenOriginal.FechaOrden = ordenModificado.FechaOrden is null ? ordenOriginal.FechaOrden : ordenModificado.FechaOrden;
                ordenOriginal.ComentariosOrden = ordenModificado.ComentariosOrden is null ? ordenOriginal.ComentariosOrden : ordenModificado.ComentariosOrden;
                ordenOriginal.HoraEntregaOrden = ordenModificado.HoraEntregaOrden is null ? ordenOriginal.HoraEntregaOrden : ordenModificado.HoraEntregaOrden;

                _dbContext.Ordenes.Update(ordenOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("VerificarIDOrden/{idOrden}")]
        public IActionResult VerificarIDOrden(string idOrden)
        {
            Orden ordenOriginal = _dbContext.Ordenes.Find(idOrden);
            if (ordenOriginal == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = false });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = true });
            }
        }

        [HttpDelete]
        [Route("EliminarOrden/{idOrden}")]
        public IActionResult EliminarOrden(string idOrden)
        {
            Orden ordenEliminar = _dbContext.Ordenes.Find(idOrden);
            if (ordenEliminar == null)
            {
                return BadRequest("Esa orden no existe.");
            }

            try
            {
                _dbContext.Ordenes.Remove(ordenEliminar);
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
            int indice = _dbContext.Ordenes.Count() + 1;
            string ID = "ORD";
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

