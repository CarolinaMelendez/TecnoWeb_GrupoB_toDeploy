using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class ProductManager : IProductManager
    {
        public List<Product> Products { get; set; }
        //public ProductManager(IDbLayer dbLayer)
        public ProductManager()
        {
            Products = new List<Product>()
            {
                new Product() { Name = "Polera", Type = "SOCCER", Code = "SOCCER-001",  Price = 45, Stock = 100 },
                new Product() { Name = "Corto", Type = "SOCCER", Code = "SOCCER-002",  Price = 30, Stock = 50 },
                new Product() { Name = "Tennis", Type = "BASQUET", Code = "BASQUET-001",  Price = 120, Stock = 250 },
                new Product() { Name = "Balon", Type = "BASKET", Code = "BASKET-002", Price = 50, Stock = 20 },
            };
        }
        
        public List<Product> GetProducts()
        {
            // Sacamos de una CAPA PERSISTIDA, no me alcanzo el tiempo
            //List<Product> products = _dbLayer.GetProduct();
            return Products;
        }
        public Product PostProduct(Product product)
        {
            Products.Add(product);
            return product;
            // return null;
        }
        public Product PutProduct(Product product)
        {
            return product;
            // return null;
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
