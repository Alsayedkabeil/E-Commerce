using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts
{
    public interface IGenericRepostory<TKey,TEntity>  where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false);
        Task<TEntity?> GetAsync(TKey key); // Nullable to handle not found cases
        Task<IEnumerable<TEntity>> GetAllAsync(ISpesification<TKey,TEntity> spec,bool changeTracker = false);
        Task<TEntity?> GetAsync(ISpesification<TKey, TEntity> spec); // Nullable to handle not found cases
        Task<int> GetCountWithoutPaginationAsync(ISpesification<TKey, TEntity> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
