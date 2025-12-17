using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts
{
    public interface IUnitOfWork
    {
        // Define methods for managing transactions
        // 1. Generate Any Repository:
        // TKey: Type of Primary Key
        // TEntity: Type of Entity
        IGenericRepostory<Tkey, TEntity> GetRepostory<Tkey, TEntity>() where TEntity : BaseEntity<Tkey>;
        // 2. Save Changes:
        Task<int> SaveChangesAsync();
    }
}
