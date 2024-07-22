using Customer.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Manufacturer.Application
{
    public class VehicleOrderCreatedConsumer : IConsumer<VehicleOrderCreated>
    {
        private readonly ILogger<VehicleOrderCreatedConsumer> logger;

        public VehicleOrderCreatedConsumer(ILogger<VehicleOrderCreatedConsumer> logger)
        {
            this.logger = logger;
        }

        public Task Consume(ConsumeContext<VehicleOrderCreated> context)
        {
            logger.LogInformation("Consuming message {messageName} with ID {vehicleOrderId}, for customer {customerId}",
                nameof(VehicleOrderCreated), context.Message.VehicleOrderId, context.Message.CustomerId);

            return Task.CompletedTask;
        }
    }
}
