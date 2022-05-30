using System;
using System.Collections.Generic;
using System.Text;

namespace DB
{
    public class UnitOfWork : IUnitOfWork
    {
        private P2DbContext _context;

        private ProductRepository _productRepository;
        private AddresssRepository _addresssRepository;

        public ProductRepository ProductRepository
        {
            get
            {
                return _productRepository;
            }
        }

        public AddresssRepository AddresssRepository
        {
            get
            {
                return _addresssRepository;
            }
        }

        public UnitOfWork(P2DbContext context)
        {
            _context = context;
            _productRepository = new ProductRepository(_context);
            _addresssRepository = new AddresssRepository(_context);
        }
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public void Save()
        {
            try
            {
                BeginTransaction();
                _context.SaveChanges();
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                throw;
            }
        }
    }
}
