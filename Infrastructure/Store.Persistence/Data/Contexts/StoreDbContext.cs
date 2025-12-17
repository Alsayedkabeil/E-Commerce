using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence.Data.Contexts
{
    public class StoreDbContext : DbContext
    {
        // CLR 
        #region Constructor
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
            
        }
        #endregion

        #region Apply Configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region Tables In DataBase
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        #endregion
    }
}
