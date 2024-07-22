using Customer.Application.Ports;
using Customer.Core;
using Microsoft.Extensions.Logging;

namespace Customer.Application.UseCases
{
    public class GetMatchingPreassemlbedVehiclesForOrder
    {
        private readonly ILogger<GetMatchingPreassemlbedVehiclesForOrder> logger;
        private readonly IVehicleOrderRepository vehicleOrderRepository;
        private readonly IWarehouseRepository warehouseRepository;

        public GetMatchingPreassemlbedVehiclesForOrder(
            ILogger<GetMatchingPreassemlbedVehiclesForOrder> logger,
            IVehicleOrderRepository vehicleOrderRepository,
            IWarehouseRepository warehouseRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.vehicleOrderRepository = vehicleOrderRepository ?? throw new ArgumentNullException(nameof(vehicleOrderRepository));
            this.warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
        }

        public async Task<List<PreassembledVehicle>> ExecuteAsync(Guid vehicleOrderId)
        {
            var vehicleOrder = await vehicleOrderRepository.GetVehicleOrder(vehicleOrderId);
            var warehouse = await warehouseRepository.GetWarehouse(Guid.NewGuid());

            return warehouse.FindMatchingPreassemlbedVehicles(vehicleOrder.EngineId, vehicleOrder.ChassisId, vehicleOrder.OptionPackId);
        }
    }
}
