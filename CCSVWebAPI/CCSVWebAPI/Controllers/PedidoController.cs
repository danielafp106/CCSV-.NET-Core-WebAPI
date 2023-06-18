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
                pedidos = _dbContext.Pedidos.Include(p => p.Proveedor).Include(p=>p.PreciosProductos).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = pedidos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = pedidos });
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
                pedido = _dbContext.Pedidos.Include(p => p.Proveedor).Include(p => p.PreciosProductos).Where(p => p.IdPedido == idPedido).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = pedido });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = pedido });
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

                if (CalcularTotales(pedido.IdPedido) == false)
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

                if (CalcularTotales(pedidoModificado.IdPedido) == false)
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

                if (CalcularTotales(idPedido) == false)
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

        [HttpPut]
        [Route("CalcularImportacion")]
        public IActionResult CalcularImportacion([FromBody] Pedido pedidoModificado)
        {
            Pedido pedidoOriginal = _dbContext.Pedidos.Find(pedidoModificado.IdPedido);
            if (pedidoOriginal == null)
            {
                return BadRequest("Ese pedido no existe.");
            }

            try
            {
                var preciosProductos = _dbContext.PreciosProductos.Where(p => p.IdPedido == pedidoModificado.IdPedido).ToList();                
              
                //Modificación
                pedidoOriginal.TotalImportePedido = pedidoModificado.TotalImportePedido is null ? pedidoOriginal.TotalImportePedido : pedidoModificado.TotalImportePedido;

                //Actualizando en pedido
                _dbContext.Pedidos.Update(pedidoOriginal);
                _dbContext.SaveChanges();


                if (CalcularTotales(pedidoModificado.IdPedido) == false)
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
                var precioImportacionUnidad = (importacionTotal != 0 && countPP != 0) ? Math.Round((decimal)(importacionTotal / countPP), 2) : 0;
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
                pedido.StockPedido = countPP;
                pedido.TotalPedido = compraTotal;
                pedido.TotalProductosPedido = sumaTotalPP;
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
