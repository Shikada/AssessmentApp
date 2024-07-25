using Customer.Application.Ports;
using Customer.Core;
using Customer.Messages.Commands;
using Manufacturer.Messages.Commands;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Customer.Application.UseCases
{
    public class ReservePartsForVehicleOrder
    {
        private readonly ILogger<GetMatchingPreassemlbedVehiclesForOrder> logger;
        private readonly IVehicleOrderRepository vehicleOrderRepository;
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IPaymentService paymentService;
        private readonly IPublishEndpoint publishEndpoint;

        public ReservePartsForVehicleOrder(
            ILogger<GetMatchingPreassemlbedVehiclesForOrder> logger,
            IVehicleOrderRepository vehicleOrderRepository,
            IWarehouseRepository warehouseRepository,
            IPaymentService paymentService,
            IPublishEndpoint publishEndpoint)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.vehicleOrderRepository = vehicleOrderRepository ?? throw new ArgumentNullException(nameof(vehicleOrderRepository));
            this.warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
            this.paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task Execute(Guid vehicleOrderId)
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

            vehicleOrder.AwaitPayment(invoice);
            var unavailableParts = warehouse.ReserveParts(vehicleOrder);

            if (unavailableParts.EngineId is not null)
            {
                vehicleOrder.AddPartAwaitingManufacture(unavailableParts.EngineId.Value);
                await publishEndpoint.Publish(new ManufactureEngine
                {
                    VehicleOrderId = vehicleOrderId,
                    EngineId = unavailableParts.EngineId.Value
                });
            }

            if (unavailableParts.ChassisId is not null)
            {
                vehicleOrder.AddPartAwaitingManufacture(unavailableParts.ChassisId.Value);
                await publishEndpoint.Publish(new ManufactureChassis
                {
                    VehicleOrderId = vehicleOrderId,
                    ChassisId = unavailableParts.ChassisId.Value
                });
            }

            if (unavailableParts.OptionPackId is not null)
            {
                vehicleOrder.AddPartAwaitingManufacture(unavailableParts.OptionPackId.Value);
                await publishEndpoint.Publish(new ManufactureOptionPack
                {
                    VehicleOrderId = vehicleOrderId,
                    OptionPackId = unavailableParts.OptionPackId.Value
                });
            }

            await vehicleOrderRepository.Save(vehicleOrder);
            await warehouseRepository.Save(warehouse);
        }
    }
}
