﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth;
using Logic;
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
            return Ok(_productManager.GetProducts());
        }

        [HttpPost]
        public IActionResult PostProducts([FromBody] Logic.Models.Product product)
        {
            return Ok(_productManager.PostProduct(product));
        }

        [HttpPut]
        public IActionResult PutProducts([FromBody] Logic.Models.Product product)
        {
            return Ok(_productManager.PutProduct(product));
        }

        [HttpDelete]
        [Route("{productId}")]
        public IActionResult DeleteProducts(Guid productId)
        {
            return Ok(_productManager.DeleteProduct(productId));
        }
    }
}
