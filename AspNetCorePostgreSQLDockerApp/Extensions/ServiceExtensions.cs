using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using AspNetCorePostgreSQLDockerApp.Repository;
using AspNetCorePostgreSQLDockerApp.Validations;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AspNetCorePostgreSQLDockerApp.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services) => services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Application API",
                Description = "Application Documentation",
                Contact = new OpenApiContact
                {
                    Name = "Kiet Pham", 
                    Email = "kietpham.dev@gmail.com", 
                    Url = new Uri("https://www.linkedin.com/in/kiet-pham-a1260b77/")
                },
                License = new OpenApiLicense
                    { Name = "MIT", Url = new Uri("https://en.wikipedia.org/wiki/MIT_License") }
            });

            // Add XML comment document by uncommenting the following
            // var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "MyApi.xml");
            // options.IncludeXmlComments(filePath);
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });
        
        public static void ConfigureCors(this IServiceCollection services) => services.AddCors(o => o.AddPolicy("AllowAllPolicy", options =>
        {
            options.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
        
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => services.AddEntityFrameworkNpgsql()
            .AddDbContext<DockerCommandsDbContext>(options =>
                options.UseNpgsql(configuration["Data:DbContext:DockerCommandsConnectionString"]))
            .AddDbContext<CustomersDbContext>(options =>
                options.UseNpgsql(configuration["Data:DbContext:CustomersConnectionString"])
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());
        
        public static void ConfigureControllersWithView(this IServiceCollection services) => services.AddControllersWithViews()
            .AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<OrderCreateValidator>())
            .AddJsonOptions(opts =>
            {
                var enumConverter = new JsonStringEnumConverter();
                opts.JsonSerializerOptions.Converters.Add(enumConverter);
            });
    }
}