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

        /// <summary>
        /// In a real application there would be some more complicated logic here, to generate an invoice
        /// and integrate with some external payment provider
        /// </summary>
        /// <param name="vehicleOrder"></param>
        /// <returns></returns>
        public async Task<Invoice?> CreateNewInvoice(VehicleOrder vehicleOrder)
        {
            var warehouse = await warehouseRepository.GetWarehouse(Warehouse.MainWarehouseId);
            
            if (warehouse == null)
                return null;

            return new Invoice(Guid.NewGuid(), vehicleOrder, warehouse.GetPrice(vehicleOrder));
        }

        /// <summary>
        /// Method with empty implementation, just for ilustrative purposes.
        /// </summary>
        /// <param name="invoice"></param>
        public Task ProcessPayedInvoice(Invoice invoice)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Method with empty implementation, just for ilustrative purposes.
        /// </summary>
        /// <param name="invoice"></param>
        public Task ProcessInvoiceCancelled(Invoice invoice)
        {
            return Task.CompletedTask;
        }
    }
}
