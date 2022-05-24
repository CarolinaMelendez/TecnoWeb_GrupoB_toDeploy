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

        public ProductsController(IProductManager productManager)
        {
            _productManager = productManager;
        }


        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productManager.GetProducts());
        }
        [HttpPost]
        public IActionResult PostProduct()
        {
            return Ok();
        }
        [HttpPut]
        public IActionResult PutProduct()
        {
            return Ok();
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
                return BadRequest("El Productp no ha sido encontrado");
            }
        }
    }
}
