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
    public class PedidoController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public PedidoController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerPedidos")]
        public IActionResult ObtenerPedidos()
        {
            List<Pedido> pedidos = new List<Pedido>();
            try
            {
                pedidos = _dbContext.Pedidos.Include(p => p.Proveedor).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = pedidos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = pedidos });
            }
        }

        [HttpGet]
        [Route("ObtenerPedido/{idPedido}")]
        public IActionResult ObtenerPedido(string idPedido)
        {
            Pedido pedido = _dbContext.Pedidos.Find(idPedido);
            if (pedido == null)
            {
                return BadRequest("Ese pedido no existe.");
            }
            try
            {
                pedido = _dbContext.Pedidos.Include(p => p.Proveedor).Where(p => p.IdPedido == idPedido).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = pedido });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = pedido });
            }
        }

        [HttpPost]
        [Route("GuardarPedido")]
        public IActionResult GuardarPedido([FromBody] Pedido pedido)
        {
            try
            {
                string ID = GenerarID();
                pedido.IdPedido = ID;
                _dbContext.Pedidos.Add(pedido);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

          [HttpPut]
        [Route("EditarPedido")]
        public IActionResult EditarPedido([FromBody] Pedido pedidoModificado)
        {
            Pedido pedidoOriginal = _dbContext.Pedidos.Find(pedidoModificado.IdPedido);
            if (pedidoOriginal == null)
            {
                return BadRequest("Ese pedido no existe.");
            }

            try
            {
                pedidoOriginal.FechaOrdenado = pedidoModificado.FechaOrdenado is null ? pedidoOriginal.FechaOrdenado : pedidoModificado.FechaOrdenado;
                pedidoOriginal.FechaRecibido = pedidoModificado.FechaRecibido is null ? pedidoOriginal.FechaRecibido : pedidoModificado.FechaRecibido;
                pedidoOriginal.TotalProductosPedido = pedidoModificado.TotalProductosPedido is null ? pedidoOriginal.TotalProductosPedido : pedidoModificado.TotalProductosPedido;
                pedidoOriginal.TotalImportePedido = pedidoModificado.TotalImportePedido is null ? pedidoOriginal.TotalImportePedido : pedidoModificado.TotalImportePedido;
                pedidoOriginal.TotalPedido = pedidoModificado.TotalPedido is null ? pedidoOriginal.TotalPedido : pedidoModificado.TotalPedido;
                pedidoOriginal.StockPedido = pedidoModificado.StockPedido is null ? pedidoOriginal.StockPedido : pedidoModificado.StockPedido;
                pedidoOriginal.IdProveedor = pedidoModificado.IdProveedor is null ? pedidoOriginal.IdProveedor : pedidoModificado.IdProveedor;

                _dbContext.Pedidos.Update(pedidoOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarPedido/{idPedido}")]
        public IActionResult EliminarPedido(string idPedido)
        {
            Pedido pedidoEliminar = _dbContext.Pedidos.Find(idPedido);
            if (pedidoEliminar == null)
            {
                return BadRequest("Ese pedido no existe.");
            }

            try
            {
                _dbContext.Pedidos.Remove(pedidoEliminar);
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
            int indice = _dbContext.Pedidos.Count() + 1;
            string ID = "PDO";
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
