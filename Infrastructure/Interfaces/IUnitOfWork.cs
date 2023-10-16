using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Category> Category { get; }

        public IGenericRepository<Manufacturer> Manufacturer { get; }

        public IGenericRepository<Product> Product { get; }

        //add other models/tables here as create them so unitofwork has access to them.

        //save changes to the data source.

        int Commit();
        Task<int> CommitAsync();
    }
}
