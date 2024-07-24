using Customer.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Manufacturer.Application.Consumers
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

            // Some logic could go here, probably some analytics that Manufacturer wants to capture for Vehicle Orders
            // This happens before any parts that are out of stock in the Customer Warehouse are actually ordered for production

            return Task.CompletedTask;
        }
    }
}
