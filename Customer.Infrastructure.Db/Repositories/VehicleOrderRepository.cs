using Customer.Core;
using Customer.Application.Ports;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.Db.Repositories
{
    public class VehicleOrderRepository : IVehicleOrderRepository
    {
        private readonly CustomerDbContext customerDbContext;

        public VehicleOrderRepository(CustomerDbContext customerDbContext)
        {
            this.customerDbContext = customerDbContext;
        }

        public async Task<VehicleOrder?> GetById(Guid id)
        {
            return await customerDbContext.VehicleOrders
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<VehicleOrder?> GetByInvoiceId(Guid invoiceId)
        {
            var invoice = await customerDbContext.Invoices.FindAsync(invoiceId);

            if (invoice is null)
                return null;

            return await customerDbContext.VehicleOrders
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(x => x.Id == invoice.VehicleOrderId);
        }

        public async Task<VehicleOrder?> Save(VehicleOrder vehicleOrder)
        {
            var persistedVehicleOrder = await customerDbContext.VehicleOrders.FindAsync(vehicleOrder.Id);

            if (persistedVehicleOrder is null)
                customerDbContext.VehicleOrders.Add(vehicleOrder);

            await customerDbContext.SaveChangesAsync();

            return await customerDbContext.VehicleOrders.FindAsync(vehicleOrder.Id);
        }
    }
}
