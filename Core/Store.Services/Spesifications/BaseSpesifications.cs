using Store.Domain.Contracts;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Spesifications
{
    public class BaseSpesifications<TKey, TEntity> : ISpesification<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public BaseSpesifications(Expression<Func<TEntity, bool>>? expression) // Constructor to set the filtering expression
        {
            Filteration = expression; // Assign the filtering expression to the Filteration property
        }

        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>(); // Initialize Includes as an empty list
        public Expression<Func<TEntity, bool>>? Filteration { get; set; } // Filtering condition
        public Expression<Func<TEntity, object>>? OrderByAscending { get; set; } // Order by ascending
        public Expression<Func<TEntity, object>>? OrderByDescending { get; set; } // Order by descending
        public int Skip { get; set; } // For pagination
        public int Take { get; set; } // For pagination
        public bool IsPagination { get; set; } // Enable/Disable pagination

        public void AddOrderByAscending(Expression<Func<TEntity, object>>? expression) // Method to set ascending order
        {
            OrderByAscending = expression; // Assign the provided expression to OrderByAscending
        }

        public void AddOrderByDescending(Expression<Func<TEntity, object>>? expression) // Method to set ascending order
        {
            OrderByDescending = expression; // Assign the provided expression to OrderByAscending
        }

        public void ApplyPagination(int PageIndex, int PageSize) // Method to enable pagination
        {
            Skip = (PageIndex-1)*PageSize; // Set the number of records to skip
            Take = PageSize; // Set the number of records to take
            IsPagination = true; // Enable pagination
        }
    }
}
