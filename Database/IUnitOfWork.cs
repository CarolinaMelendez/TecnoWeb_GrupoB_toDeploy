using System;
using System.Collections.Generic;
using System.Text;

namespace DB
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();

        // FROM HERE
        void Save();

        ProductRepository ProductRepository { get; }
    }
}
