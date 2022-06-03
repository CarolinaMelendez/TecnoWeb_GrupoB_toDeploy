using DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DB
{
    public class ProductRepository
    {
        private PIDbContext _context;

        public ProductRepository(PIDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Set<Product>().ToListAsync();
        }

        public Product CreateProduct(Product product)
        {
            _context.Set<Product>().Add(product);
            return product;
        }

        public Product GetById(Guid id)
        {
            return _context.Set<Product>().Find(id);
        }

        public Product UpdateProduct (Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            return product;
        }

        public Product DeleteProduct(Product product)
        {
            _context.Set<Product>().Remove(product);
            return product;
        }
    }
}