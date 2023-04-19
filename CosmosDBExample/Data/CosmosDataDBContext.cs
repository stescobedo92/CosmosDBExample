using CosmosDBExample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDBExample.Data
{
    public class CosmosDataDBContext : DbContext
    {
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Customer>? Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos("accountEndpoint", "accountKey","databaseName");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configuring Employees
            modelBuilder.Entity<Employee>()
                    .ToContainer("Employees") // ToContainer
                    .HasPartitionKey(e => e.Id); // Partition Key

            // configuring Customers
            modelBuilder.Entity<Customer>()
                .ToContainer("Customers") // ToContainer
                .HasPartitionKey(c => c.Id); // Partition Key

            modelBuilder.Entity<Customer>().OwnsMany(p => p.Orders);
        }
    }
}
