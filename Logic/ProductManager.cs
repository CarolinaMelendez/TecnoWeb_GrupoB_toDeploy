using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class ProductManager : IProductManager
    {
        public List<Product> Products { get; set; }

        public ProductManager()
        {
            Products = new List<Product>
            {
                new Product() { Name = "Polera", Type = "SOCCER", Code = "SOCCER-001",  Stock = 10 },
                new Product() { Name = "Balon", Type = "BASKET", Code = "BASKET-001",  Stock = 10 }
            };
        }
        
        public List<Product> GetProducts()
        {
            return null;
        }
        public Product PostProduct()
        {
            return null;
        }
        public Product PutProduct()
        {
            return null;
        }
        public Product DeleteProduct(Product product)
        {
            var productToDelete = Products.Find(x => x.Code == product.Code);
            if (productToDelete != null)
            {
                Products.Remove(productToDelete);
            }
            return productToDelete;
        }
    }
}
