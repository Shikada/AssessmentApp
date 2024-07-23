using Customer.Application.Ports;
using Customer.Core;
using Microsoft.Extensions.Logging;

namespace Customer.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private ILogger<PaymentService> logger;
        private IWarehouseRepository warehouseRepository;

        public PaymentService(ILogger<PaymentService> logger, IWarehouseRepository warehouseRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
        }

        public async Task<Invoice?> CreateNewInvoice(VehicleOrder vehicleOrder)
        {
            var warehouse = await warehouseRepository.GetWarehouse(Warehouse.MainWarehouseId);
            
            if (warehouse == null)
                return null;

            return new Invoice(Guid.NewGuid(), vehicleOrder, warehouse.GetPrice(vehicleOrder));
        }
    }
}
