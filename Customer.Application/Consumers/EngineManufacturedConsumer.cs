using Customer.Application.UseCases;
using Manufacturer.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Customer.Application.Consumers
{
    public class EngineManufacturedConsumer : IConsumer<EngineManufactured>
    {
        private readonly ILogger<EngineManufacturedConsumer> logger;
        private readonly AssociateManufacturedPartWithVehileOrder associateManufacturedEngineWithVehileOrder;

        public EngineManufacturedConsumer(
            ILogger<EngineManufacturedConsumer> logger,
            AssociateManufacturedPartWithVehileOrder associateManufacturedEngineWithVehileOrder)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.associateManufacturedEngineWithVehileOrder = associateManufacturedEngineWithVehileOrder
                ?? throw new ArgumentNullException(nameof(associateManufacturedEngineWithVehileOrder));
        }

        public async Task Consume(ConsumeContext<EngineManufactured> context)
        {
            await associateManufacturedEngineWithVehileOrder.Execute(context.Message.VehicleOrderId, context.Message.EngineId);
        }
    }
}
