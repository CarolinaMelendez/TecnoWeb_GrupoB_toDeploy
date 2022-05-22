using System;
using System.Collections.Generic;

namespace Logic
{
    public interface IProductManager
    {
        public List<Product> GetProducts();
        public Product PostProduct();
        public Product PutProduct();
        public Product DeleteProduct(Product product);
    }
}
