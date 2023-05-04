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
    public class TipoPagoController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public TipoPagoController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerTiposPagos")]
        public IActionResult ObtenerTiposPagos()
        {
            List<TipoPago> tipopagos = new List<TipoPago>();
            try
            {
                tipopagos = _dbContext.TiposPagos.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = tipopagos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = tipopagos });
            }
        }

        [HttpGet]
        [Route("ObtenerTipoPago/{idTipoPago}")]
        public IActionResult ObtenerTipoPago(string idTipoPago)
        {
            TipoPago tipopago = _dbContext.TiposPagos.Find(idTipoPago);
            if (tipopago == null)
            {
                return BadRequest("Ese tipo de pago no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = tipopago });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = tipopago });
            }
        }

        [HttpPost]
        [Route("GuardarTipoPago")]
        public IActionResult GuardarTipoPago([FromBody] TipoPago tipopago)
        {
            try
            {
                _dbContext.TiposPagos.Add(tipopago);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarTipoPago")]
        public IActionResult EditarTipoPago([FromBody] TipoPago tipopagoModificado)
        {
            TipoPago tipopagoOriginal = _dbContext.TiposPagos.Find(tipopagoModificado.IdTipoPago);
            if (tipopagoOriginal == null)
            {
                return BadRequest("Ese tipo de pago no existe.");
            }

            try
            {
                tipopagoOriginal.NombreTipoPago = tipopagoModificado.NombreTipoPago is null ? tipopagoOriginal.NombreTipoPago : tipopagoModificado.NombreTipoPago;

                _dbContext.TiposPagos.Update(tipopagoOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("VerificarIDTipoPago/{idTP}")]
        public IActionResult VerificarIDTipoPago(string idTP)
        {
            TipoPago tipopagoOriginal = _dbContext.TiposPagos.Find(idTP);
            if (tipopagoOriginal == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = false });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { IdExists = true });
            }
        }

        [HttpDelete]
        [Route("EliminarTipoPago/{idTipoPago}")]
        public IActionResult EliminarTipoPago(string idTipoPago)
        {
            TipoPago tipopagoEliminar = _dbContext.TiposPagos.Find(idTipoPago);
            if (tipopagoEliminar == null)
            {
                return BadRequest("Esa tipo de pago no existe.");
            }

            try
            {
                _dbContext.TiposPagos.Remove(tipopagoEliminar);
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
