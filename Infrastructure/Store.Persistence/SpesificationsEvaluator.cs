using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.Domain.Contracts;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence
{
    // This class is intended to evaluate specifications for querying entities
    public static class SpesificationsEvaluator
    {
        // Input Query Static =>  _context.Products
        // IQueryable<TEntity> InputQuery => Input Static
        // Generate Dynamic Query:
        public static IQueryable<TEntity> GetDynamicQuery<TKey, TEntity>(IQueryable<TEntity> InputQuery,ISpesification<TKey, TEntity> spec) where TEntity : BaseEntity<TKey>
        {
            var query = InputQuery; // _context.Products

            if (spec.Filteration is not null) // if any Filteration
            {
                query = query.Where(spec.Filteration);
            }

            if (spec.OrderByAscending is not null)
            {
                query = query.OrderBy(spec.OrderByAscending);
            }
            else if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }


            query = spec.Includes.Aggregate(query, (query, includeExpression) => query.Include(includeExpression));
            

            return query;
        }
    }
}
