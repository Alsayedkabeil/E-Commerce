
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Contracts;
using Store.Persistence;
using Store.Persistence.Data.Contexts;
using Store.Persistence.Refactoring_To_DataBase;
using Store.Services;
using Store.Services.Abstractions;
using Store.Services.Mapping.products;
using Store.Services.Refactoring_To_Services;
using Store.Shared.ErrorModels;
using Store.Web.Extensions;
using Store.Web.Middlewares;
using System.Threading.Tasks;

namespace Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAllServices(builder.Configuration); // Call the extension method to add all services

            var app = builder.Build();

            await app.AddMiddleWares(); // Call the extension method to add all middlewares

            app.Run();
        }
    }
}
