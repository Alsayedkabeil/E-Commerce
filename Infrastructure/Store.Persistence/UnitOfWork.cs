using Store.Domain.Contracts;
using Store.Domain.Entities;
using Store.Persistence.Data.Contexts;
using Store.Persistence.Reposetories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence
{
    public class UnitOfWork(StoreDbContext _context) : IUnitOfWork
    {
        // Cache for repositories
        // Key: Entity Type Name
        // Value: Repository Instance
        //private Dictionary<string, object> _repositories = new Dictionary<string, object>();
        private ConcurrentDictionary<string, object> _repositories = new ConcurrentDictionary<string, object>();
        public IGenericRepostory<Tkey, TEntity> GetRepostory<Tkey, TEntity>() where TEntity : BaseEntity<Tkey>
        {
            //var typeName = typeof(TEntity).Name; // Get the name of the entity type
            //if (!_repositories.ContainsKey(typeName))
            //{
            //    var repository = new GenericRepostory<Tkey, TEntity>(_context); // Create a new repository instance
            //}
            //return (IGenericRepostory<Tkey, TEntity>)_repositories[typeName];
            return (IGenericRepostory<Tkey, TEntity>)_repositories.GetOrAdd(typeof(TEntity).Name, 
                new GenericRepostory<Tkey, TEntity>(_context)); // Create or get the repository instance in a thread-safe manner
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();  // Save all changes made in this context to the database
        }
    }
}
