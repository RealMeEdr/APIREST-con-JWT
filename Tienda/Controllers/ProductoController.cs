using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tienda.Data;
using System.Data.SqlClient;
using System.Data;
using Tienda.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace Tienda.Controllers
{
    [EnableCors("ReglasCors")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        [Route("Productos")]
        public async Task<IActionResult> Productos()
        {
            try
            {
                List<ProductoModel> products = new List<ProductoModel>();
                ConexionData db = new ConexionData();
                ResponseModel responseM = await db.GetAllProductAsync();
                if (responseM.IsSuccess)
                {
                    products = (List<ProductoModel>) responseM.ObjectResponse;
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = $"{responseM.MessageException + "\n" + responseM.MessageStackTrace}", response = "" });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = products });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"{e.Message}" + "\n"+$"{e.StackTrace}", response = "" });
            }
        }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> Producto(int Id)
        {
            try
            {
                ProductoModel product = new ProductoModel();
                ConexionData db = new ConexionData();
                ResponseModel responseM = await db.GetProductAsync(Id);
                if (responseM.IsSuccess)
                {
                    product = (ProductoModel)responseM.ObjectResponse;
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = $"{responseM.MessageException + "\n" + responseM.MessageStackTrace}", response = "" });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = product });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"{e.Message}" + "\n" + $"{e.StackTrace}", response = "" });
            }
        }
        
        [HttpPost]
        [Route("CrearProducto")]
        public async Task<IActionResult> CrateProduct([FromBody] ProductoModel product)
        {
            try
            {
                ConexionData db = new ConexionData();
                ResponseModel responseM = await db.PostProducto(product);
                if (responseM.IsSuccess)
                {
                    product = (ProductoModel)responseM.ObjectResponse;
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = $"{responseM.MessageException + "\n" + responseM.MessageStackTrace}", response = "" });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK, Se ha guardado exitosamente" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"{e.Message}" + "\n" + $"{e.StackTrace}", response = "" });
            }
        }

        [HttpPut]
        [Route("EditarProducto")]
        public async Task<IActionResult> EditProduct([FromBody] ProductoModel product)
        {
            try
            {
                ConexionData db = new ConexionData();
                ResponseModel responseM = await db.EditProduct(product);
                if (responseM.IsSuccess)
                    product = (ProductoModel)responseM.ObjectResponse;
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = $"{responseM.MessageException + "\n" + responseM.MessageStackTrace}", response = "" });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK, Se ha modificado los valores del producto" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"{e.Message}" + "\n" + $"{e.StackTrace}", response = "" });
            }
        }
        
        [HttpDelete]
        [Route("EliminarProducto/{Id:int}")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            try
            {
                ConexionData db = new ConexionData();
                ResponseModel responseM = await db.DeleteProduct(Id);
                if (responseM.IsSuccess)
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK, Eliminado Satistactoriamente"});
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = $"{responseM.MessageException + "\n" + responseM.MessageStackTrace}", response = "" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"{e.Message}" + "\n" + $"{e.StackTrace}", response = "" });
            }
        }
    }
}
