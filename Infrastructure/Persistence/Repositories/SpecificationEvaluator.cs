using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecifictation<TEntity, TKey> specifictation) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
            if (specifictation.Criteria != null)
            {
                query = query.Where(specifictation.Criteria);
            }

            if(specifictation.OrderBy != null)
            {
                query = query.OrderBy(specifictation.OrderBy);
            }
            else if (specifictation.OrderByDescending != null)
            {
                query = query.OrderByDescending(specifictation.OrderByDescending);
            }

            if (specifictation.IncludeExpression != null && specifictation.IncludeExpression.Count > 0)
            {
                query = specifictation.IncludeExpression.Aggregate(query, (current, includeExp) => current.Include(includeExp));  
            }

            if(specifictation.IsPaginated)
            {
                query = query.Skip(specifictation.Skip).Take(specifictation.Take);
            }

            return query;
        }

    }
}
