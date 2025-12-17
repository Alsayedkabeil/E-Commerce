using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Store.Domain.Contracts;
using Store.Persistence.Data.Contexts;
using Store.Persistence.IdentityData.Contexts;
using Store.Persistence.Reposetories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence.Refactoring_To_DataBase
{
    public static class RefactorAnyThingAboutDataBase
    {
        public static IServiceCollection RefactorDataBase(this IServiceCollection services,IConfiguration configuration)
        {
            // Here you can add any database-related refactoring or configuration
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); // Adjust the connection string name as needed
            });
            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")); // Adjust the connection string name as needed
            });
            services.AddScoped<IDbinitializer, Dbinitializer>(); // Register Dbinitializer for dependency injection
            services.AddScoped<IUnitOfWork, UnitOfWork>(); // Register UnitOfWork for dependency injection
            services.AddScoped<IBasketRepostory, BasketRepostory>(); // Register BasketRepostory for dependency injection
            services.AddScoped<IChachingRepostory, ChachingRepostory>(); // Register ChachingRepostory for dependency injection
            services.AddSingleton< IConnectionMultiplexer>((serviceProvider) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!); // Adjust the connection string name as needed
            });
            return services;
        }
    }
}
