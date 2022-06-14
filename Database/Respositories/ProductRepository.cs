using DB.Exceptions;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
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

        public Product GetById(Guid id)
        {
            try
            {
                Log.Information($"DB-Repository Layer: El trata de obtener el ID={id.ToString()}");
                return _context.Set<Product>().Find(id);
            }
            catch (Exception ex)
            {
                string err = "HUBO FALLAS al obtener el producto de la base de datos";
                // Console.WriteLine(err);
                // Console.WriteLine(ex.Message + ex.StackTrace); // debido al uso de log, no creo que so ya sea necesario. Pendiente hacer pruebas
                throw new DatabaseException($"{err} : {ex.Message}");
            }
        }
        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                Log.Information($"DB-Repository Layer: Se trata de obtener los products ");
                return await _context.Set<Product>().ToListAsync();
            }
            catch (Exception ex)
            {
                string err = "HUBO FALLAS al obtener los productos de la base de datos";
                // Console.WriteLine(err);
                // Console.WriteLine(ex.Message + ex.StackTrace);
                throw new DatabaseException($"{err} : {ex.Message}");
            }
           
        }

        public Product CreateProduct(Product product)
        {
            try
            {
                Log.Information($"DB-Repository Layer: Se trata de almacenar un nuevo producto");
                _context.Set<Product>().Add(product);
              
                return product;
            }
            catch (Exception ex)
            {
                string err = "HUBO FALLAS al crear el producto en la base de datos";
                // Console.WriteLine(err);
                // Console.WriteLine(ex.Message + ex.StackTrace);
                throw new DatabaseException($"{err} : {ex.Message}");
            }
            
        }

        public Product UpdateProduct(Product product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                Log.Information($"DB-Repository Layer:Se trata de actualizar los datos del producto");
                return product;
            }
            catch (Exception ex)
            {
                string err = "HUBO FALLAS al actualizar el producto en la base de datos";
                Console.WriteLine(err);
                Console.WriteLine(ex.Message + ex.StackTrace);
                throw new DatabaseException($"{err} : {ex.Message}");
            }
        }

        public Product DeleteProduct(Product product)
        {
            try
            {
                _context.Set<Product>().Remove(product);
                Log.Information($"DB-Repository Layer: Se trata de eliminar un producto");
                return product;
            }
            catch (Exception ex)
            {
                string err = "HUBO FALLAS al eliminar el producto en la base de datos";
                Console.WriteLine(err);
                Console.WriteLine(ex.Message + ex.StackTrace);
                throw new DatabaseException($"{err} : {ex.Message}");
            }
        }
    }
}