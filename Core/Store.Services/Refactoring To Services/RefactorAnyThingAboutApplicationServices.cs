using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Services.Abstractions;
using Store.Services.Mapping.Basket;
using Store.Services.Mapping.Orders;
using Store.Services.Mapping.products;
using Store.Services.Mapping.Security;
using Store.Shared.Dtos.OptionsPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Refactoring_To_Services
{
    public static class RefactorAnyThingAboutApplicationServices
    {
        public static IServiceCollection RefactorServices(this IServiceCollection services,IConfiguration configuration)
        {
            // Here you can add any application service-related refactoring or configuration
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration))); // Register AutoMapper with ProductProfile
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile())); // Register AutoMapper with BasketProfile
            services.AddAutoMapper(M => M.AddProfile(new OrderProfile())); // Register AutoMapper with OrderProfile
            services.AddAutoMapper(M => M.AddProfile(new AuthProfile())); // Register AutoMapper with AuthProfile
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;
        }
    }
}
