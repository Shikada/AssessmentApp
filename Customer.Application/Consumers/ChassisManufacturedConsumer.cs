using Customer.Application.UseCases;
using Manufacturer.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Customer.Application.Consumers
{
    public class ChassisManufacturedConsumer : IConsumer<ChassisManufactured>
    {
        private readonly ILogger<ChassisManufacturedConsumer> logger;
        private readonly AssociateManufacturedPartWithVehileOrder associateManufacturedEngineWithVehileOrder;

        public ChassisManufacturedConsumer(
            ILogger<ChassisManufacturedConsumer> logger,
            AssociateManufacturedPartWithVehileOrder associateManufacturedEngineWithVehileOrder)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.associateManufacturedEngineWithVehileOrder = associateManufacturedEngineWithVehileOrder
                ?? throw new ArgumentNullException(nameof(associateManufacturedEngineWithVehileOrder));
        }

        public async Task Consume(ConsumeContext<ChassisManufactured> context)
        {
            await associateManufacturedEngineWithVehileOrder.Execute(context.Message.VehicleOrderId, context.Message.ChassisId);
        }
    }
}
