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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var engine1 = new Engine("EP1", "Petrol 1.2L", 90, 3, 0, 2000);
            //var engine2 = new Engine("EP2", "Petrol 1.6L", 120, 5, 0, 3000);
            //var engine3 = new Engine("ED1", "Diesel 1.5L", 120, 2, 0, 2500);

            //var chassis1 = new Chassis("Model 1", "Hatchback", 8, 0, 15000);
            //var chassis2 = new Chassis("Model 2", "Sedan", 4, 0, 19000);
            //var chassis3 = new Chassis("Model 3", "SUV", 10, 0, 22000);

            //var optionPack1 = new OptionPack("Comfort", 12, 0, 1200);
            //var optionPack2 = new OptionPack("Elegance", 10, 0, 3100);
            //var optionPack3 = new OptionPack("Executive", 4, 0, 4800);

            //var preassambledVehicle1 = new PreassembledVehicle(engine1.Id, chassis2.Id, optionPack2.Id, 3, 0, 25000);
            //var preassambledVehicle2 = new PreassembledVehicle(engine3.Id, chassis3.Id, optionPack3.Id, 2, 0, 35000);

            //var customer1 = new Core.Customer("John", "Doe", "Random Street");
            //var customer2 = new Core.Customer("Steve", "Jobless", "Pear Street");

            //modelBuilder
            //    .Entity<Engine>()
            //    .HasData(engine1, engine2, engine3);

            //modelBuilder
            //    .Entity<Chassis>()
            //    .HasData(chassis1, chassis2, chassis3);

            //modelBuilder
            //    .Entity<OptionPack>()
            //    .HasData(optionPack1, optionPack2, optionPack3);

            //modelBuilder
            //    .Entity<PreassembledVehicle>()
            //    .HasData(preassambledVehicle1, preassambledVehicle2);

            //modelBuilder
            //    .Entity<Core.Customer>()
            //    .HasData(customer1, customer2);

            //modelBuilder
            //    .Entity<Warehouse>()
            //    .HasData(new Warehouse(
            //        new List<Engine> { engine1, engine2, engine3 },
            //        new List<Chassis> { chassis1, chassis2, chassis3 },
            //        new List<OptionPack> { optionPack1, optionPack2, optionPack3 },
            //        new List<PreassembledVehicle> { preassambledVehicle1, preassambledVehicle2 }));
        }
    }
}
