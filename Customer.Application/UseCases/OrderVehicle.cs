﻿using Customer.Application.Ports;
using Customer.Core;
using Microsoft.Extensions.Logging;

namespace Customer.Application.UseCases
{
    public class OrderVehicle
    {
        private readonly ILogger<OrderVehicle> logger;
        private readonly IVehicleOrderRepository vehicleOrderRepository;
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IPaymentService paymentService;

        public OrderVehicle(
            ILogger<OrderVehicle> logger,
            IVehicleOrderRepository vehicleOrderRepository,
            IWarehouseRepository warehouseRepository,
            IPaymentService paymentService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.vehicleOrderRepository = vehicleOrderRepository ?? throw new ArgumentNullException(nameof(vehicleOrderRepository));
            this.warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
            this.paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        }

        public async Task<bool> Execute(Guid invoiceId)
        {
            var vehicleOrder = await vehicleOrderRepository.GetByInvoiceId(invoiceId);
            var warehouse = await warehouseRepository.GetWarehouse(Warehouse.MainWarehouseId);

            if (vehicleOrder is null)
            {
                logger.LogError("Could not get a vehicle order for invoice with ID {invoiceId} from database that should exist", invoiceId);
                throw new Exception($"Could not get a vehicle order for invoice with ID {invoiceId} from database that should exist");
            }

            if (warehouse is null)
            {
                logger.LogError("Could not get a warehouse with ID {warehouseId} from database that should exist", Warehouse.MainWarehouseId);
                throw new Exception($"Could not get a warehouse with ID {Warehouse.MainWarehouseId} from database that should exist");
            }

            vehicleOrder.Accept();
            await paymentService.ProcessPayedInvoice(vehicleOrder.Invoice!); // safe to ignore null check here
            var isSuccessful = warehouse.AcceptOrder(vehicleOrder);

            await vehicleOrderRepository.Save(vehicleOrder);
            await warehouseRepository.Save(warehouse);

            return isSuccessful;
        }
    }
}
