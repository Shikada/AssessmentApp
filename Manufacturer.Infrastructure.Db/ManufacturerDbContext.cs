using Manufacturer.Core;
using Microsoft.EntityFrameworkCore;

namespace Manufacturer.Infrastructure.Db
{
    public  class ManufacturerDbContext : DbContext
    {
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Chassis> AllChassis { get; set; }
        public DbSet<OptionPack> OptionPacks { get; set; }
        public DbSet<EngineManufactureItem> EngineManufactureItems { get; set; }
        public DbSet<ChassisManufactureItem> ChassisManufactureItems { get; set; }
        public DbSet<OptionPackManufactureItem> OptionPackManufactureItems { get; set; }

        public ManufacturerDbContext(DbContextOptions<ManufacturerDbContext> options)
            : base(options)
        { }
    }
}
