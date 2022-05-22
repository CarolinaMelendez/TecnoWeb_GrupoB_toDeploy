using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return Ok();
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
            return Ok();
        }
    }
}
