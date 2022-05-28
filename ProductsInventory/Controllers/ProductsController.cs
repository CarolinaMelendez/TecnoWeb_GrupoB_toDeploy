using Auth;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductManager _productManager;
        private ISessionManager _sessionManager;

        public ProductsController(IProductManager productManager, ISessionManager sessionManager)
        {
            // this = _(evita ambiguedades)
            _productManager = productManager;
            _sessionManager = sessionManager;
        }

        [HttpGet]
        public IActionResult GetProducts([FromHeader] string userName, [FromHeader] string password)
        {
            if (_sessionManager.ValidateCredentials(userName, password) != null)
            {
                //var productList = _productManager.GetProducts();
                return Ok(_productManager.GetProducts());
            }
            else
            {
                return Unauthorized();
            }
        }
        [HttpGet("withoutAuth")]
        public IActionResult GetProducts()
        {
            return Ok(_productManager.GetProducts());
        }

        [HttpPost]
        public IActionResult PostProduct([FromHeader] string userName, [FromHeader] string password, [FromBody] Product product)
        {
            if (_sessionManager.ValidateCredentials(userName, password) != null)
            {
                return Ok(_productManager.PostProduct(product));
            }
            else
            {
                return Unauthorized();
            }
        }

        // Caro: para hacer mis pruebas, por el momente este hice sin seguridad
        [HttpPost("withoutAuth")]
        public IActionResult PostProduct([FromBody] Product product)
        {
            return Ok(_productManager.PostProduct(product));
        }

        [HttpPut]
        public IActionResult PutProduct(Product product)
        {
            /* OTRA MANERA DE ESCRIBIR
            if(user.Ci <= 0 || user.Name == null || user.Name.Trim() == "")
            {

            }*/
            //Se identifico una manera mas detallada e interesante
            
            if(product.Stock <= 0 || String.IsNullOrEmpty(product.Name))
            {

            }

            Product updated = _productManager.PutProduct(product);
            if (updated != null)
            {
                return Ok(updated);
            }
            else
            {
                return BadRequest("< No se encuentra el producto >");
            }
        }

        [HttpDelete]
        // Se deberia protejer el borrado de los productos por Seguridad !!!
        public IActionResult DeleteProduct(Product product)
        {
            Product ProductDeleted = _productManager.DeleteProduct(product);

            if (ProductDeleted != null)
            {
                return Ok(ProductDeleted);
            }
            else
            {
                return BadRequest("< El Producto no ha sido encontrado >");
            }
        }
    }
}
