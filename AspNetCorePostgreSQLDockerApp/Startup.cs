using System;
using AspNetCorePostgreSQLDockerApp.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AspNetCorePostgreSQLDockerApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add PostgreSQL support
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<DockerCommandsDbContext>(options =>
                    options.UseNpgsql(Configuration["Data:DbContext:DockerCommandsConnectionString"]))
                .AddDbContext<CustomersDbContext>(options =>
                    options.UseNpgsql(Configuration["Data:DbContext:CustomersConnectionString"]));


            services.AddControllersWithViews();

            // Add our PostgreSQL Repositories (scoped to each request)
            services.AddScoped<IDockerCommandsRepository, DockerCommandsRepository>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();

            //Transient: Created each time they're needed
            services.AddTransient<DockerCommandsDbSeeder>();
            services.AddTransient<CustomersDbSeeder>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Application API",
                    Description = "Application Documentation",
                    Contact = new OpenApiContact { Name = "Author" },
                    License = new OpenApiLicense
                        { Name = "MIT", Url = new Uri("https://en.wikipedia.org/wiki/MIT_License") }
                });

                // Add XML comment document by uncommenting the following
                // var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "MyApi.xml");
                // options.IncludeXmlComments(filePath);
            });

            services.AddCors(o => o.AddPolicy("AllowAllPolicy", options =>
            {
                options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "Client/dist"; });

            // services.AddRouting(options => options.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            DockerCommandsDbSeeder dockerCommandsDbSeeder, CustomersDbSeeder customersDbSeeder)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseCors("AllowAllPolicy");

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            // Visit http://localhost:5000/swagger
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action}/{id?}");

                // Handle redirecting client-side routes to Customers/Index route
                endpoints.MapFallbackToController("Index", "Customers");
            });
            
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "Client";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4201");
                }
            });

            customersDbSeeder.SeedAsync(app.ApplicationServices).Wait();
            dockerCommandsDbSeeder.SeedAsync(app.ApplicationServices).Wait();
        }
    }
}