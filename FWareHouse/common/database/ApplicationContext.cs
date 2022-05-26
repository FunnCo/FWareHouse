using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWareHouse.common.entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FWareHouse.common.database
{
    class ApplicationContext: DbContext
    {
        
        public DbSet<Partner> partner { get; set; }
        public DbSet<Employee> employee { get; set; }
        public DbSet<TransportCompany> transport_company { get; set; }

        public DbSet<StoredProduct> products_current_info { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        public void executeNonReturningQuery(string query)
        {
           Database.ExecuteSqlRaw(query);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = "server=26.147.199.238;user=Fox;password=8rSPsJE8XBDKIJ0J;database=warehouse";
            var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));
            optionsBuilder.UseMySql(connectionString, serverVersion).LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }

}
