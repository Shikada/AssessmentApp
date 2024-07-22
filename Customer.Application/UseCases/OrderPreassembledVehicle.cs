using Customer.Application.Ports;
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
            var warehouse = await warehouseRepository.GetWarehouse(Guid.NewGuid());

            vehicleOrder.AcceptOrder();
            warehouse.OrderPreassembledVehicle(preassembledVehicleId);
            
            await vehicleOrderRepository.Save(vehicleOrder);
            await warehouseRepository.Save(warehouse);
        }
    }
}
