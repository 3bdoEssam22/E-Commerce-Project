using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey id);

        #region With Specifications
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifictation<TEntity, TKey> specifictations);
        Task<TEntity?> GetByIdAsync(ISpecifictation<TEntity, TKey> specifictations);

        #endregion        

        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
