using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manufacturer.Infrastructure.Db
{
    public class ManufacturerDbContextFactory : IDesignTimeDbContextFactory<ManufacturerDbContext>
    {
        public ManufacturerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ManufacturerDbContext>()
                                    .UseSqlite("Data Source=../WebApi/Database/manufacturer.db")
                                    .EnableSensitiveDataLogging();

            return new ManufacturerDbContext(optionsBuilder.Options);
        }
    }
}
