using Customer.Core;
using Customer.Application.Ports;

namespace Customer.Infrastructure.Db.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        public async Task<Warehouse> GetWarehouse(Guid id)
        {
            return new Warehouse();
        }
    }
}
