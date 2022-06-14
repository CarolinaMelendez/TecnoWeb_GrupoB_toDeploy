using DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB
{
    public class PIDbContext : DbContext
    {
        private IConfiguration _configuration;

        public DbSet<Product> Product { get; set; }

        public PIDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetSection("Database").GetSection("ConnectionString").Value;
            Log.Information($"Se obtiene la cadena de conexión de la base de datos: {connectionString}");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
