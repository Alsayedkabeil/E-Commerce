using Microsoft.EntityFrameworkCore;
using Store.Domain.Contracts;
using Store.Domain.Entities;
using Store.Domain.Entities.Products;
using Store.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence.Reposetories
{
    public class GenericRepostory<Tkey, TEntity>(StoreDbContext _context) : IGenericRepostory<Tkey, TEntity> where TEntity : BaseEntity<Tkey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return changeTracker
               ? await _context.Products.Include(B => B.Brand).Include(T => T.Type).ToListAsync() as IEnumerable<TEntity>
               : await _context.Products.Include(B => B.Brand).Include(T => T.Type).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }
            return changeTracker
                ? await _context.Set<TEntity>().ToListAsync()
                : await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        public async Task<TEntity?> GetAsync(Tkey key)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _context.Products
                    .Include(B => B.Brand)
                    .Include(T => T.Type)
                    .FirstOrDefaultAsync(e => e.Id == key as int?) as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(key);
        }
        private IQueryable<TEntity> ApplySpecification(ISpesification<Tkey, TEntity> spec)
        {
            return SpesificationsEvaluator.GetDynamicQuery(_context.Set<TEntity>(), spec);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpesification<Tkey, TEntity> spec, bool changeTracker = false)
        {
           return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(ISpesification<Tkey, TEntity> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Update(entity); 
        }
        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task<int> GetCountWithoutPaginationAsync(ISpesification<Tkey, TEntity> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
    }
}
