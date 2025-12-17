using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Contracts;
using Store.Domain.Entities.Identity;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Products;
using Store.Persistence.Data.Contexts;
using Store.Persistence.IdentityData.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Persistence
{
    public class Dbinitializer(
        StoreDbContext _context,
        StoreIdentityDbContext _store,
        UserManager<AppUser> _userManager,
        RoleManager<IdentityRole> _roleManager
        ) : IDbinitializer
    {
        // Use Primary Constructor Injection to inject StoreDbContext


        //private readonly StoreDbContext _context;

        //public Dbinitializer(StoreDbContext context)
        //{
        //    _context = context;
        //}
        #region InitializeAsync
        public async Task InitializeAsync()
        {
            // Implementation for database initialization logic goes here

            // 1. Create database schema if it doesn't exist
            // 2. Update database schema to the latest version
            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any()) // Check for pending migrations
            {
                await _context.Database.MigrateAsync(); // Apply pending migrations +  Create database if it doesn't exist
            }
            // 3. Seed initial data

            #region Seed DeliveryMethod
            if (!_context.DeliveryMethods.Any())
            {
                // 3.1 Seed DeliveryMethods
                // 3.1.1 Read data from a JSON file (delivery):
                var delivery_data = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Persistence\Data\DataSeeding\delivery.json");
                // 3.1.2 Deserialize JSON data to a list of deliveries entities:
                var deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(delivery_data);
                // 3.1.3 Add the deliveries to the database if they don't already exist:
                if (deliveries is not null && deliveries.Count > 0)
                {
                    await _context.DeliveryMethods.AddRangeAsync(deliveries);
                }
            }
            #endregion

            #region Seed ProductBrands
            if (!_context.ProductBrands.Any())
            {
                // 3.1 Seed ProductBrands
                // 3.1.1 Read data from a JSON file (brands):
                var brand_data = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Persistence\Data\DataSeeding\brands.json");
                // 3.1.2 Deserialize JSON data to a list of ProductBrand entities:
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brand_data);
                // 3.1.3 Add the brands to the database if they don't already exist:
                if (brands is not null && brands.Count > 0)
                {
                    await _context.ProductBrands.AddRangeAsync(brands);
                }
            }
            #endregion

            #region Seed ProductTypes
            // 3.2 Seed ProductTypes 
            if (!_context.ProductTypes.Any())
            {
                // 3.2.1 Read data from a JSON file (types):
                var types_data = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Persistence\Data\DataSeeding\types.json");
                // 3.2.2 Deserialize JSON data to a list of ProductType entities:
                var types = JsonSerializer.Deserialize<List<ProductType>>(types_data);
                // 3.2.3 Add the types to the database if they don't already exist:
                if (types is not null && types.Count > 0)
                {
                    await _context.ProductTypes.AddRangeAsync(types);
                }
            }
            #endregion

            #region Seed Products
            // 3.3 Seed Products 
            if (!_context.Products.Any())
            {
                // 3.3.1 Read data from a JSON file (products):
                var products_data = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Persistence\Data\DataSeeding\products.json");
                // 3.3.2 Deserialize JSON data to a list of Product entities:
                var products = JsonSerializer.Deserialize<List<Product>>(products_data);
                // 3.3.3 Add the products to the database if they don't already exist:
                if (products is not null && products.Count > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                }
            }
            #endregion

            await _context.SaveChangesAsync(); // Save all changes to the database
        }
        #endregion

        #region InitializeIdentityAsync
        public async Task InitializeIdentityAsync()
        {
            // Implementation for database initialization logic goes here

            // 1. Create database schema if it doesn't exist
            // 2. Update database schema to the latest version
            if (_store.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any()) // Check for pending migrations
            {
                await _store.Database.MigrateAsync(); // Apply pending migrations +  Create database if it doesn't exist
            }
            // 3. Seed initial data
            // 3.1 Seed Roles
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "SuperAdmin",
                });
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin",
                });
            }
            // 3.2 Seed Users
            if (!_userManager.Users.Any()) // If there are no users in the database
            {
                var superAdminUser = new AppUser()
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "0123456789",
                };
                var adminUser = new AppUser()
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "0123456789",
                };
                await _userManager.CreateAsync(superAdminUser, "P@ssW0rd");
                await _userManager.CreateAsync(adminUser, "P@ssW0rd");
                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
        #endregion
    }
}
