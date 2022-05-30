using DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DB
{
    public class ProductRepository
    {
        private P2DbContext _context;

        public ProductRepository(P2DbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Set<Product>().ToListAsync();
        }

        public Product CreateUser(Product product)
        {
            _context.Set<Product>().Add(product);
            return product;
        }

        public Product GetById(Guid id)
        {
            return _context.Set<Product>().Find(id);
        }

        public Product UpdateUser(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            return product;
        }

        public Product DeleteUser(Product product)
        {
            _context.Set<Product>().Remove(product);
            return product;
        }
    }
}