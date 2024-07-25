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
        private readonly IVehicleOrderRepository vehicleOrderRepository;

        public CreateVehicleOrder(
            ILogger<CreateVehicleOrder> logger,
            IPublishEndpoint publishEndpoint,
            IVehicleOrderRepository vehicleOrderRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            this.vehicleOrderRepository = vehicleOrderRepository ?? throw new ArgumentNullException(nameof(vehicleOrderRepository));
        }

        public async Task<VehicleOrder> ExecuteAsync(Messages.Commands.CreateVehicleOrder command)
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

            await vehicleOrderRepository.Save(newVehicleOrder);

            return newVehicleOrder;
        }
    }
}
