using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Microsoft.Extensions.Configuration;

namespace Data.Context
{
    public class EmployeeReportsContext : DbContext
    {
        private static readonly string connectionString = "";
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
