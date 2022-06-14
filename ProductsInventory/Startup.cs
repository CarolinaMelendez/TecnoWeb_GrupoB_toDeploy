using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth;
using DB;
using Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProductsInventory.Middlewares;
using Serilog;

namespace ProductsInventory
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                // no habia para enriquecer con detalle de exception
                .Enrich.WithMachineName() // Enriquecer con el nombre de la maquina
                .Enrich.WithEnvironmentName() // Tener el ambiente que se ha hecho
                .CreateLogger();
            Log.Information("Configuración de LOG fue existosamente inicializado");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            services.AddTransient<IProductManager, ProductManager>();
            //services.AddSingleton<IProductManager, ProductManager>();


            services.AddTransient<ISessionManager, SessionManager>();
            services.AddTransient<Services.PriceService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<PIDbContext>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                );
                /* options.AddPolicy("AllowOrigins-QA",
                    builder => builder
                        // .AllowAnyOrigin()
                        .WithOrigins("192.168.0.1", "...")
                        // .AllowAnyHeader()
                        .WithHeaders("username", "password", "content-type")
                        // .AllowAnyMethod()
                        .WithMethods("GET", "POST", "PUT")
                );
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                ); */
            });
               
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"{Configuration.GetSection("Application").GetSection("Title").Value} {groupName}",
                    Version = groupName,
                    Description = "Practice 3 - Group 2 API",
                    Contact = new OpenApiContact
                    {
                        Name = "Group 2",
                        Email = string.Empty,
                        Url = new Uri("https://group2.com"),
                    }
                });
            });

            Log.Information("Configuración de servicios fue existosamente finalizado");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGroupTwoExceptionMiddleware();

            // app.UseHsts();

            // app.UseHttpsRedirection();

            // app.UseStaticFiles(); // FOR MVC

            app.UseRouting();

            app.UseCors("AllowAnyOrigin");

            // app.UseAuthorization();

            // app.UseAuthentication();

            app.UseAuthenticationMiddleware();

            // app.UseDemoCustomTwoMiddleware();

            app.UseSwagger(); 
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Group 2 API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Log.Information("Configuración básica fue existosamente finalizado");
        }
    }
}
