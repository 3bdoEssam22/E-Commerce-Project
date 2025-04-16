using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            //Get Type Name
            var typeName = typeof(TEntity).Name;
            //Dic<string, object> string key [NameOfType] -- Object Object From Generic Repository.
            if (_repositories.TryGetValue(typeName, out object? value))
                return (IGenericRepository<TEntity, TKey>)value;
            else
            {
                //Create Object
                var repo = new GenericRepository<TEntity, TKey>(_dbContext);
                //Store Object In Dictionary
                _repositories["typeName"] = repo;
                //Return Object
                return repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
