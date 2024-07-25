using Customer.Core;
using Customer.Application.Ports;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.Db.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly CustomerDbContext customerDbContext;

        public WarehouseRepository(CustomerDbContext customerDbContext)
        {
            this.customerDbContext = customerDbContext;
        }

        public async Task<Warehouse?> GetWarehouse(Guid id)
        {
            return await customerDbContext.Warehouses
                .Include(p => p.Engines)
                .Include(p => p.AllChassis)
                .Include(p => p.OptionPacks)
                .Include(p => p.PreassembledVehicles)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Warehouse?> Save(Warehouse warehouse)
        {
            var persistedWarehouse = await customerDbContext.Warehouses.FindAsync(warehouse.Id);

            if (persistedWarehouse is null)
                customerDbContext.Warehouses.Add(warehouse);

            await customerDbContext.SaveChangesAsync();

            return await customerDbContext.Warehouses.FindAsync(warehouse.Id);
        }
    }
}
