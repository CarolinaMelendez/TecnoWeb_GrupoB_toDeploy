using System;
using System.Collections.Generic;

namespace Logic
{
    public interface IProductManager
    {
        public List<Product> GetProducts();
        public Product PostProduct(Product product);
        public Product PutProduct(Product product);
        public Product DeleteProduct(Product product);
    }
}
