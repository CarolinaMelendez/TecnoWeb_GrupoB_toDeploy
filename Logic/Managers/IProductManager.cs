using Logic.Models;
using System;
using System.Collections.Generic;

namespace Logic
{
    public interface IProductManager
    {
        public List<Product> GetProducts();
        public Product PostProduct(Logic.Models.Product product);
        public Product PutProduct(Logic.Models.Product product);
        public Product DeleteProduct(Guid productId);
    }
}
