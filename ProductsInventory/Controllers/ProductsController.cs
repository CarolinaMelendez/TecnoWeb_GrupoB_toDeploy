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
            _productManager = productManager;
            _sessionManager = sessionManager;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productManager.GetProducts());
        }

        [HttpPost]
        public IActionResult PostProduct(Product product)
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
