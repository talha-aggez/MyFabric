using Core.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Context
{
    public class StoreContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-V6I8AQ6; database=dbMyFabric; integrated security=true;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SubProductTree> SubProductTrees { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<WorkCenter> WorkCenters { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<WorkCenterOperation> WorkCenterOperations { get; set; }
    }
}
