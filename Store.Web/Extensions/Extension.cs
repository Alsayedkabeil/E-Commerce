using Azure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Store.Domain.Contracts;
using Store.Domain.Entities.Identity;
using Store.Persistence.IdentityData.Contexts;
using Store.Persistence.Refactoring_To_DataBase;
using Store.Services.Refactoring_To_Services;
using Store.Shared.Dtos.OptionsPattern;
using Store.Shared.ErrorModels;
using Store.Web.Middlewares;
using System.Text;

namespace Store.Web.Extensions
{
    public static class Extension
    {
        #region Add All Services Before Build
        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWebServices(); // Call the private method to add web-specific services
            services.AddIdentityServices(); // Call the private method to add identity services

            #region RefactorAnyThingAboutDataBase
            services.RefactorDataBase(configuration); // Call the extension method to refactor database-related services
            #endregion

            #region RefactorAnyThingAboutApplicationServices
            services.RefactorServices(configuration); // Call the extension method to refactor application-related services
            #endregion

            #region Configuration MiddleWare With Special Behaviour
            services.AddConfigurationApiBehaviorOptions(); // Call the private method to configure ApiBehaviorOptions
            #endregion

            #region Service To Add Authentication
            services.AddJwtServices(configuration);
            #endregion

            #region Add CROS To Allow Any FrontEnd Use My BackEnd
            services.AddCors(Options => // Enable CORS
            {
                Options.AddPolicy("AllowAll", Policy => // Define a CORS policy named "AllowAll"
                {
                    Policy.AllowAnyOrigin() // Allow requests from any origin
                           .AllowAnyMethod() // Allow any HTTP method
                           .AllowAnyHeader(); // Allow any HTTP header
                });
            });

            #endregion

            return services;
        }

        #region Private Method To Add Identity Services
        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();
            return services;
        }
        #endregion

        #region Private Method To Add Authentication
        private static IServiceCollection AddJwtServices(this IServiceCollection services,IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });
            return services;
        }
        #endregion

        #region Private Method To Add WebServices
        private static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            // Add web-specific services here if needed in the future
            // Register your services here
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        #endregion

        #region Private To Add Configuration<ApiBehaviorOptions>
        private static IServiceCollection AddConfigurationApiBehaviorOptions(this IServiceCollection services)
        {
            #region Configuration MiddleWare With Special Behaviour
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState
                        .Where(E => E.Value.Errors.Any())
                        .Select(E => new ErrorListResponse()
                        {
                            Field = E.Key,
                            Errors = E.Value.Errors.Select(error => error.ErrorMessage)
                        });
                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            #endregion
            return services;
        }

        #endregion

        #endregion

        #region Add All Services After Build
        public static async Task<WebApplication> AddMiddleWares(this WebApplication app)
        {

            await app.SeedingData(); // Seed the database with initial data

            app.UseGlobalErrorHandlingMiddleware(); // Use custom global error handling middleware

            app.UseStaticFiles(); // Enable serving static files from wwwroot

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll"); // Use CORS policy to allow any origin, method, and header
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();


            return app;
        }

        #region Private Method To Seed DataBase
        private static async Task<WebApplication> SeedingData(this WebApplication app)
        {
            #region Ask CLR
            using var scope = app.Services.CreateScope(); // Create a scope to resolve scoped services
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbinitializer>(); // Resolve IDbinitializer
            await dbInitializer.InitializeAsync(); // Call InitializeAsync to set up the database 
            await dbInitializer.InitializeIdentityAsync(); // Call InitializeIdentityAsync to set up identity-related data
            return app;
            #endregion
        }
        #endregion

        #region Private To Add Special MiddleWare
        private static WebApplication UseGlobalErrorHandlingMiddleware(this WebApplication app)
        {
            #region Configuration => Middleware
            app.UseMiddleware<GlobalErrorHandlingMiddleware>(); // Use custom global error handling middleware
            return app;
            #endregion
        }
        #endregion

        #endregion
    }
}
