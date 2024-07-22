using Customer.Application.Ports;
using Customer.Core;
using Customer.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Customer.Application.UseCases
{
    public class CreateVehicleOrder
    {
        private readonly ILogger<CreateVehicleOrder> logger;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IWarehouseRepository warehouseRepository;

        public CreateVehicleOrder(
            ILogger<CreateVehicleOrder> logger,
            IPublishEndpoint publishEndpoint,
            IWarehouseRepository warehouseRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            this.warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
        }

        public async Task ExecuteAsync(Messages.Commands.CreateVehicleOrder command)
        {
            var newVehicleOrder = new VehicleOrder(
                command.CustomerId,
                command.EngineId,
                command.ChassisId,
                command.OptionPackId);

            await publishEndpoint.Publish(new VehicleOrderCreated
            {
                VehicleOrderId = newVehicleOrder.Id,
                CustomerId = command.CustomerId
            });

            var warehouse = await warehouseRepository.GetWarehouse(Warehouse.MainWarehouseId);
        }
    }
}
