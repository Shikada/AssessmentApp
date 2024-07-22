using Customer.Application.Ports;
using Customer.Core;
using Microsoft.Extensions.Logging;

namespace Customer.Application.UseCases
{
    public class OrderPreassembledVehicle
    {
        private readonly ILogger<GetMatchingPreassemlbedVehiclesForOrder> logger;
        private readonly IVehicleOrderRepository vehicleOrderRepository;
        private readonly IWarehouseRepository warehouseRepository;

        public OrderPreassembledVehicle(
            ILogger<GetMatchingPreassemlbedVehiclesForOrder> logger,
            IVehicleOrderRepository vehicleOrderRepository,
            IWarehouseRepository warehouseRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.vehicleOrderRepository = vehicleOrderRepository ?? throw new ArgumentNullException(nameof(vehicleOrderRepository));
            this.warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
        }

        public async Task Execute(Guid vehicleOrderId, Guid preassembledVehicleId)
        {
            var vehicleOrder = await vehicleOrderRepository.GetVehicleOrder(vehicleOrderId);
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

            vehicleOrder.AcceptOrder();
            warehouse.OrderPreassembledVehicle(preassembledVehicleId);
            
            await vehicleOrderRepository.Save(vehicleOrder);
            await warehouseRepository.Save(warehouse);
        }
    }
}
