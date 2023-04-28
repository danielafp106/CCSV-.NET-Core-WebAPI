using CCSVWebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCSVWebAPI.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        public readonly CcsvdbContext _dbContext;

        public ProveedorController(CcsvdbContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("ObtenerProveedores")]
        public IActionResult ObtenerProveedores()
        {
            List<Proveedor> proveedor = new List<Proveedor>();
            try
            {
                proveedor = _dbContext.Proveedores.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = proveedor });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = proveedor });
            }
        }

        [HttpGet]
        [Route("ObtenerProveedor/{idProveedor}")]
        public IActionResult ObtenerProveedor(string idProveedor)
        {
            Proveedor proveedor = _dbContext.Proveedores.Find(idProveedor);
            if (proveedor == null)
            {
                return BadRequest("Ese proveedor no existe.");
            }
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = proveedor });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = proveedor });
            }
        }

        [HttpPost]
        [Route("GuardarProveedor")]
        public IActionResult GuardarProveedor([FromBody] Proveedor proveedor)
        {
            try
            {
                string ID = GenerarID();
                proveedor.IdProveedor = ID;
                _dbContext.Proveedores.Add(proveedor);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarProveedor")]
        public IActionResult EditarProveedor([FromBody] Proveedor proveedorModificado)
        {
            Proveedor proveedorOriginal = _dbContext.Proveedores.Find(proveedorModificado.IdProveedor);
            if (proveedorOriginal == null)
            {
                return BadRequest("Ese proveedor no existe.");
            }

            try
            {
                proveedorOriginal.Comentarios = proveedorModificado.Comentarios is null ? proveedorOriginal.Comentarios : proveedorModificado.Comentarios;
                proveedorOriginal.NombreProveedor = proveedorModificado.NombreProveedor is null ? proveedorOriginal.NombreProveedor : proveedorModificado.NombreProveedor;
                proveedorOriginal.ContactoProveedor = proveedorModificado.ContactoProveedor is null ? proveedorOriginal.ContactoProveedor : proveedorModificado.ContactoProveedor;

                _dbContext.Proveedores.Update(proveedorOriginal);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarProveedor/{idProveedor}")]
        public IActionResult EliminarProveedor(string idProveedor)
        {
            Proveedor proveedorEliminar = _dbContext.Proveedores.Find(idProveedor);
            if (proveedorEliminar == null)
            {
                return BadRequest("Ese corte no existe.");
            }

            try
            {
                _dbContext.Proveedores.Remove(proveedorEliminar);
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
            int indice = _dbContext.Proveedores.Count() + 1;
            string ID = "PVD";
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
