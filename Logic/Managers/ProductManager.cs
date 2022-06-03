using System;
using System.Collections.Generic;
using DB;
using Logic.Models;
using Services;
using DB.Models;

namespace Logic
{
    public class ProductManager : IProductManager
    {
        public List<Logic.Models.Product> Products { get; set; }
        private GetPriceService _getPriceService;
        private IUnitOfWork _uow;

        //public ProductManager(IDbLayer dbLayer)
        public ProductManager(GetPriceService getPriceService, IUnitOfWork uow)
        {
            _getPriceService = getPriceService;
            _uow = uow;
            //_idNumberService = idNumberService;
            Products = new List<Logic.Models.Product>()
            {
                new Logic.Models.Product() { Name = "Polera", Type = "SOCCER", Code = "SOCCER-001",  Price = 45, Stock = 100 },
                new Logic.Models.Product() { Name = "Corto", Type = "SOCCER", Code = "SOCCER-002",  Price = 30, Stock = 50 },
                new Logic.Models.Product() { Name = "Tennis", Type = "BASQUET", Code = "BASQUET-001",  Price = 120, Stock = 250 },
                new Logic.Models.Product() { Name = "Balon", Type = "BASKET", Code = "BASKET-002", Price = 50, Stock = 20 }
            };
        }

        /*
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
        */
        
        public List<Logic.Models.Product> GetProducts()
        {
            List<DB.Models.Product> products = _uow.ProductRepository.GetAllProducts().Result;

            List<Logic.Models.Product> productsConverted = new List<Models.Product>();
            foreach (DB.Models.Product item in products)
            {
                productsConverted.Add(new Logic.Models.Product() { Id = item.Id, Name = item.Name, Type = item.Type, Code = item.Code, Stock = item.Stock });
            }

            return productsConverted;
        }

        
        public Logic.Models.Product PostProduct(Logic.Models.Product product)
        {
            int newPriceGenerate = _getPriceService.GetPriceServiceAsync().Result ;
            DB.Models.Product productConverted = new DB.Models.Product()
            {
                Name = product.Name,
                Type = product.Type,
                Code = getNewCode(product.Type),
                Stock = product.Stock,
                Price = newPriceGenerate,
                Id = new Guid()
            };
            productConverted = _uow.ProductRepository.CreateProduct(productConverted);
            _uow.Save();

            return product;
        }

        public string getNewCode(string typeProduct)
        {
            string newNumberOfCode = "";
            int nextNumber = 0;
            // implementar excepcion si el usuario inserta un tipo diferente de SOCCER OF BASKET
            
            List<Logic.Models.Product> listOneType = GetProducts().FindAll(product => product.Type == typeProduct);

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
            }
            else if (nextNumber < 100)
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

            return new Logic.Models.Product() { Name = productFound.Name, Type = productFound.Type, Id = productFound.Id, Code = productFound.Code, Stock = productFound.Stock };
        }
    }
}
