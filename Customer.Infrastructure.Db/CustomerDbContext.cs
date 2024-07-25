using Customer.Core;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.Db
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Core.Customer> Customers { get; set; }
        public DbSet<VehicleOrder> VehicleOrders { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Chassis> AllChassis { get; set; }
        public DbSet<OptionPack> OptionPacks { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        { }
    }
}
