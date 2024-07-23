using Customer.Core;
using Customer.Application.Ports;

namespace Customer.Infrastructure.Db.Repositories
{
    public class VehicleOrderRepository : IVehicleOrderRepository
    {
        public async Task<VehicleOrder?> GetVehicleOrder(Guid id)
        {
            return new VehicleOrder();
        }

        public async Task<VehicleOrder?> GetVehicleOrderByInvoiceId(Guid invoiceId)
        {
            return new VehicleOrder();
        }

        public async Task<VehicleOrder?> Save(VehicleOrder vehicleOrder)
        {
            return vehicleOrder;
        }
    }
}
