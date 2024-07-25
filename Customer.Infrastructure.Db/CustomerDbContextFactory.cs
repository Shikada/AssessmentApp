using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.Db
{
    public class CustomerDbContextFactory : IDesignTimeDbContextFactory<CustomerDbContext>
    {
        public CustomerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerDbContext>()
                                    .UseSqlite("Data Source=../WebApi/Database/customer.db")
                                    .EnableSensitiveDataLogging();

            return new CustomerDbContext(optionsBuilder.Options);
        }
    }
}
