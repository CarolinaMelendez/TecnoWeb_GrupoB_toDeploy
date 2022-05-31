using System;
using System.Collections.Generic;
using DB;
using Logic.Models;
using Services;
// using DBLayer.Models;
// using DBLayer;

namespace Logic
{
    public class ProductManager : IProductManager
    {
        public List<Product> Products { get; set; }
        private IdNumberService _idNumberService;
        private IUnitOfWork _uow;


        // ---> Descomentar cuando ya este la conexión con la DB

        /*
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
        }*/


        // --->  Mientras tanto
        public ProductManager()
        {
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
            // ---> Descomentar cuando ya este la conexión con la DB

            /*
            // List<DBLayer.Models.User> users = _dbLayer.GetUser();
            // return Users;
            List<DB.Models.Product> products = _uow.ProductRepository.GetAllProducts().Result;

            List<Logic.Models.Product> productsConverted = new List<Models.Product>();
            foreach (DB.Models.Product item in products)
            {
                productsConverted.Add(new Logic.Models.Product() { Name = item.Name, Type = item.Type, Code = item.Code, Price = item.Price, Stock = item.Stock });
            }

            return productsConverted;
            */

            // --->  Mientras tanto
            return Products;
        }

        
        public Logic.Models.Product PostProduct(Logic.Models.Product product)
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


            // ---> Descomentar cuando ya este la conexión con la DB (lo de abajo)
            /*
            DB.Models.Product productConverted = new DB.Models.Product()
            {
                Name = product.Name,
                Type = product.Type,
                Code = product.Code,
                Price = product.Price,
                Stock = product.Stock,
                Id = new Guid()
            };
            productConverted = _uow.ProductRepository.CreateProduct(productConverted);
            _uow.Save();

            return product;
            */

            // --->  Mientras tanto
            product.Code = getNewCode(product.Type);
            Products.Add(product);
            return product;
        }

        public string getNewCode (string typeProduct)
        {
            string newNumberOfCode = "";
            int nextNumber = 0;
            // implementar excepcion si el usuario inserta un tipo diferente de SOCCER OF BASKET
            List<Logic.Models.Product> listOneType = Products.FindAll(product => product.Type == typeProduct);

            if (listOneType.Count == 0)
            {
                return typeProduct + "-001";
            }
            else
            {
                string lastCode = listOneType[listOneType.Count - 1].Code;
                nextNumber = Int32.Parse(lastCode.Split('-')[1]) + 1;
            }

            if (nextNumber < 10)
            {
                newNumberOfCode = "-00" + nextNumber;
            }else if (nextNumber < 100)
            {
                newNumberOfCode = "-0" + nextNumber;
            }
            else
            {
                newNumberOfCode = "-" + nextNumber;
            }
            // implementar excepcion si hay más de 999 
            return typeProduct + newNumberOfCode;
        }

        public Logic.Models.Product PutProduct(Logic.Models.Product product)
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
        public Logic.Models.Product DeleteProduct(Guid productId)
        {
            DB.Models.Product productFound = _uow.ProductRepository.GetById(productId);

            _uow.ProductRepository.DeleteProduct(productFound);
            _uow.Save();

            return new Logic.Models.Product() { Name = productFound.Name, Type = productFound.Type, Id = productFound.Id, Code = productFound.Code, Price = productFound.Price, Stock = productFound.Stock };
        }
    }
}
