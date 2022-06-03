using System;
using System.Collections.Generic;
using System.Text;

namespace DB
{
    public class UnitOfWork : IUnitOfWork
    {
        private PIDbContext _context;

        private ProductRepository _productRepository;

        public ProductRepository ProductRepository
        {
            get
            {
                return _productRepository;
            }
        }

        public UnitOfWork(PIDbContext context)
        {
            _context = context;
            _productRepository = new ProductRepository(_context);
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
