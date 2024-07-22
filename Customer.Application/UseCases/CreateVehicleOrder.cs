using Customer.Core;
using Customer.Messages.Commands;
using Customer.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Customer.Application.UseCases
{
    public class CreateVehicleOrder
    {
        private readonly ILogger<CreateVehicleOrder> logger;
        private readonly IPublishEndpoint publishEndpoint;

        public CreateVehicleOrder(
            ILogger<CreateVehicleOrder> logger,
            IPublishEndpoint publishEndpoint)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
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
        }
    }
}
