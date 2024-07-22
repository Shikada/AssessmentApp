using Customer.Core;
using Customer.Application.Ports;

namespace Customer.Infrastructure.Db.Repositories
{
    public class VehicleOrderRepository : IVehicleOrderRepository
    {
        public async Task<VehicleOrder> GetVehicleOrder(Guid id)
        {
            return new VehicleOrder();
        }
    }
}
