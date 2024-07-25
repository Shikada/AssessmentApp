using Customer.Application.Ports;
using Customer.Core;
using Microsoft.Extensions.Logging;

namespace Customer.Application.UseCases
{
    public class ReservePreassembledVehicleForPayment
    {
        private readonly ILogger<GetMatchingPreassemlbedVehiclesForOrder> logger;
        private readonly IVehicleOrderRepository vehicleOrderRepository;
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IPaymentService paymentService;

        public ReservePreassembledVehicleForPayment(
            ILogger<GetMatchingPreassemlbedVehiclesForOrder> logger,
            IVehicleOrderRepository vehicleOrderRepository,
            IWarehouseRepository warehouseRepository,
            IPaymentService paymentService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.vehicleOrderRepository = vehicleOrderRepository ?? throw new ArgumentNullException(nameof(vehicleOrderRepository));
            this.warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
            this.paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        }

        public async Task<Invoice?> Execute(Guid vehicleOrderId, Guid preassembledVehicleId)
        {
            var vehicleOrder = await vehicleOrderRepository.GetById(vehicleOrderId);
            var warehouse = await warehouseRepository.GetWarehouse(Warehouse.MainWarehouseId);

            if (vehicleOrder is null)
            {
                logger.LogError("Could not get a vehicle order with ID {vehicleOrderId} from database that should exist", vehicleOrderId);
                throw new Exception($"Could not get a vehicle order with ID {vehicleOrderId} from database that should exist");
            }

            if (warehouse is null)
            {
                logger.LogError("Could not get a warehouse with ID {warehouseId} from database that should exist", Warehouse.MainWarehouseId);
                throw new Exception($"Could not get a warehouse with ID {Warehouse.MainWarehouseId} from database that should exist");
            }

            var invoice = await paymentService.CreateNewInvoice(vehicleOrder);

            if (invoice is null)
            {
                logger.LogError("Payment service failed to create an invoice for vehicle order with ID {vehicleOrderId}", vehicleOrderId);
                throw new Exception($"Payment service failed to create an invoice for vehicle order with ID {vehicleOrderId}");
            }

            vehicleOrder.AssociatePreassembledVehicle(preassembledVehicleId);
            vehicleOrder.AwaitPayment(invoice);
            var successfullyReserved = warehouse.ReservePreassembledVehicle(preassembledVehicleId);

            if (!successfullyReserved)
            {
                logger.LogInformation("Failed to reserve preassembled vehicle with ID {preassembledVehicleId}", preassembledVehicleId);
                return null;
            }
            
            await vehicleOrderRepository.Save(vehicleOrder);
            await warehouseRepository.Save(warehouse);

            return invoice;
        }
    }
}
