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
    public class PrecioProductoController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public PrecioProductoController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerPreciosProductos")]
        public IActionResult ObtenerPreciosProductos()
        {
            List<PrecioProducto> preciosproductos = new List<PrecioProducto>();
            try
            {
                preciosproductos = _dbContext.PreciosProductos.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = preciosproductos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = preciosproductos });
            }
        }

        [HttpGet]
        [Route("ObtenerPrecioProducto/{idPrecioProducto:int}")]
        public IActionResult ObtenerPrecioProducto(int idPrecioProducto)
        {
            PrecioProducto precioproducto = _dbContext.PreciosProductos.Find(idPrecioProducto);
            if (precioproducto == null)
            {
                return BadRequest("Ese registro de precios no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = precioproducto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = precioproducto });
            }
        }

        [HttpPut]
        [Route("ActualizarStockRealTime")]
        public IActionResult ActualizarStockRealTime([FromBody] PrecioProducto ppModificado)
        {
            PrecioProducto ppOriginal = _dbContext.PreciosProductos.Find(ppModificado.IdPrecioProducto);
            if (ppOriginal == null)
            {
                return BadRequest("Ese registro de precios no existe.");
            }

            try
            {
                ppOriginal.StockTotalRealTime = ppModificado.StockTotalRealTime is null ? ppOriginal.StockTotalRealTime : ppModificado.StockTotalRealTime;

                _dbContext.PreciosProductos.Update(ppOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarPrecioProducto")]
        public IActionResult EditarPrecioProducto([FromBody] PrecioProducto ppModificado)
        {
            PrecioProducto ppOriginal = _dbContext.PreciosProductos.Find(ppModificado.IdPrecioProducto);
            if (ppOriginal == null)
            {
                return BadRequest("Ese registro de precios no existe.");
            }

            try
            {
                ppOriginal.CompraTotalProducto = ppModificado.CompraTotalProducto is null ? ppOriginal.CompraTotalProducto : ppModificado.CompraTotalProducto;
                ppOriginal.CompraUnidadProducto = ppModificado.CompraUnidadProducto is null ? ppOriginal.CompraUnidadProducto : ppModificado.CompraUnidadProducto;
                ppOriginal.CompraUnidadPublico = ppModificado.CompraUnidadPublico is null ? ppOriginal.CompraUnidadPublico : ppModificado.CompraUnidadPublico;
                ppOriginal.Ganancia = ppModificado.Ganancia is null ? ppOriginal.Ganancia : ppModificado.Ganancia;
                ppOriginal.Importacion = ppModificado.Importacion is null ? ppOriginal.Importacion : ppModificado.Importacion;
                ppOriginal.Paqueteria = ppModificado.Paqueteria is null ? ppOriginal.Paqueteria : ppModificado.Paqueteria;
                ppOriginal.TarifaEnvio = ppModificado.TarifaEnvio is null ? ppOriginal.TarifaEnvio : ppModificado.TarifaEnvio;
                ppOriginal.StockTotalComprado = ppModificado.StockTotalComprado is null ? ppOriginal.StockTotalComprado : ppModificado.StockTotalComprado;
                ppOriginal.StockTotalRealTime = ppModificado.StockTotalRealTime is null ? ppOriginal.StockTotalRealTime : ppModificado.StockTotalRealTime;

                _dbContext.PreciosProductos.Update(ppOriginal);
                _dbContext.SaveChanges();

                if (CalcularTotales(ppModificado.IdPedido) == false)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Algo salió mal.." });
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }


        [HttpPost]
        [Route("GuardarPrecioProducto")]
        public IActionResult GuardarPrecioProducto([FromBody] PrecioProducto precioproducto)
        {
            try
            {
                _dbContext.PreciosProductos.Add(precioproducto);
                _dbContext.SaveChanges();

                if (CalcularTotales(precioproducto.IdPedido) == false)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Algo salió mal.." });
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarPrecioProducto/{idPrecioProducto:int}")]
        public IActionResult EliminarPrecioProducto(int idPrecioProducto)
        {
            PrecioProducto precioproducto = _dbContext.PreciosProductos.Find(idPrecioProducto);
            if (precioproducto == null)
            {
                return BadRequest("Ese registro de precios no existe.");
            }

            try
            {
                _dbContext.PreciosProductos.Remove(precioproducto);
                _dbContext.SaveChanges();

                if (CalcularTotales(precioproducto.IdPedido) == false)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Algo salió mal.." });
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        private bool CalcularTotales(string idPedido)
        {
            try
            {
                //El pedido
                Pedido pedido = _dbContext.Pedidos.Where(p => p.IdPedido == idPedido).FirstOrDefault();
                //Todos los productos del pedido
                List<PrecioProducto> todosPP = _dbContext.PreciosProductos.Where(pp => pp.IdPedido == idPedido).ToList();

                decimal compraTotal = 0;
                decimal importacionTotal = (decimal)(pedido.TotalImportePedido);
                decimal sumaTotalPP = 0;
                int countPP = (int)todosPP.Sum(p => p.StockTotalComprado);
                var precioImportacionUnidad = (importacionTotal != 0 && countPP!=0) ? Math.Round((decimal)(importacionTotal / countPP), 2) : 0;
                sumaTotalPP = sumaTotalPP + importacionTotal;

                foreach (var PP in todosPP)
                {
                    PrecioProducto actualPP = PP;
                    actualPP.Importacion = precioImportacionUnidad;
                    _dbContext.ChangeTracker.Clear();
                    _dbContext.Update(actualPP);
                    _dbContext.SaveChanges();
                    sumaTotalPP = sumaTotalPP + (decimal)actualPP.CompraTotalProducto;
                    compraTotal = compraTotal + (decimal)actualPP.CompraTotalProducto;
                }
                pedido.TotalPedido = compraTotal;
                pedido.TotalProductosPedido = sumaTotalPP;
                pedido.StockPedido = countPP;
                _dbContext.ChangeTracker.Clear();
                _dbContext.Pedidos.Update(pedido);
                _dbContext.SaveChanges();

                return true;


            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
