using System;
using System.Collections.Generic;
using Logic.Models;
using Services;
using DBLayer.Models;
using DBLayer;

namespace Logic
{
    public class ProductManager : IProductManager
    {
        public List<Product> Products { get; set; }
        private IdNumberService _idNumberService;
        private IUnitOfWork _uow;

        //public ProductManager(IDbLayer dbLayer)
        public ProductManager(IdNumberService idNumberService, IUnitOfWork uow)
        {
            _uow = uow;
            _idNumberService = idNumberService;
            Products = new List<Logic.Models.Product>()
            {
                new Logic.Models.Product() { Name = "Polera", Type = "SOCCER", Code = "SOCCER-001",  Price = 45, Stock = 100 },
                new Logic.Models.Product() { Name = "Corto", Type = "SOCCER", Code = "SOCCER-002",  Price = 30, Stock = 50 },
                new Logic.Models.Product() { Name = "Tennis", Type = "BASQUET", Code = "BASQUET-001",  Price = 120, Stock = 250 },
                new Logic.Models.Product() { Name = "Balon", Type = "BASKET", Code = "BASKET-002", Price = 50, Stock = 20 }
            };
        }
        
        public List<Logic.Models.Product> GetProducts()
        {
            // List<DBLayer.Models.User> users = _dbLayer.GetUser();
            // return Users;
            List<DB.Models.Product> products = _uow.UserRepository.GetAll().Result;

            List<Logic.Models.Product> usersConverted = new List<Models.Product>();
            foreach (DB.Models.Product item in products)
            {
                usersConverted.Add(new Logic.Models.Product() { Name = item.Name, Type = item.Type, Code = item.Code, Price = item.Price, Stock = item.Stock });
            }

            return productsConverted;
        }
        public Logic.Models.Product PostUser(Logic.Models.Product product)
        {
            /* 
             int ciParsed = 0;
             if (Int32.TryParse(product.CI, out ciParsed))
             {
                 throw new InvalidCIException();
             }
             */

            /* string idNumber = _idNumberService.GetIdNumberServiceAsync().Result;
            product.Id = idNumber;
            Products.Add(product);
            return product; */

            DB.Models.Product ProductConverted = new DB.Models.Product()
            {
                Name = product.Name,
                Type = product.Type,
                Code = product.Code,
                Price = product.Price,
                Stock = product.Stock,
                Id = new Guid()
            };
            productConverted = _uow.UserRepository.CreateProduct(productConverted);
            _uow.Save();

            return product;
        }
        public Logic.Models.Product PutUser(Logic.Models.Product product)
        {
            DB.Models.Product productFound = _uow.ProductRepository.GetById(product.Id);

            productFound.Name = product.Name;
            productFound.Type = product.Type;
            productFound.Code = product.Code;
            productFound.Price = product.Price;
            productFound.Stock = product.Stock;

            _uow.ProductRepository.UpdateProduct(productFound);
            _uow.Save();

            return product;
        }
        public Logic.Models.Product DeleteUser(Guid productId)
        {
            DB.Models.Product productFound = _uow.ProductRepository.GetById(productId);

            _uow.ProductRepository.DeleteProduct(productFound);
            _uow.Save();

            return new Logic.Models.Product() { Name = productFound.Name, Type = productFound.Type, Id = productFound.Id, Code = productFound.Code, Price = productFound.Price, Stock = productFound.Stock };
        }
    }
}
