﻿using System;
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

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = "server=192.168.31.100;user=Fox;password=8rSPsJE8XBDKIJ0J;database=warehouse";
            var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));
            optionsBuilder.UseMySql(connectionString, serverVersion).LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }

}
