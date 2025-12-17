using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts
{
    public interface ISpesification<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        List<Expression<Func<TEntity, object>>> Includes { get; set; } // Include Navigation properties
        Expression<Func<TEntity, bool>>? Filteration { get; set; } // Filtering condition
        Expression<Func<TEntity, object>>? OrderByAscending { get; set; } // Order by ascending
        Expression<Func<TEntity, object>>? OrderByDescending { get; set; } // Order by descending
        int Skip { get; set; } // For pagination
        int Take { get; set; } // For pagination
        bool IsPagination { get; set; } // Enable/Disable pagination
    }
}
