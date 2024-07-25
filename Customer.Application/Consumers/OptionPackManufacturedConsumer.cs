using Customer.Application.UseCases;
using Manufacturer.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Consumers
{
    public class OptionPackManufacturedConsumer : IConsumer<OptionPackManufactured>
    {
        private readonly ILogger<OptionPackManufacturedConsumer> logger;
        private readonly AssociateManufacturedPartWithVehileOrder associateManufacturedEngineWithVehileOrder;

        public OptionPackManufacturedConsumer(
            ILogger<OptionPackManufacturedConsumer> logger,
            AssociateManufacturedPartWithVehileOrder associateManufacturedEngineWithVehileOrder)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.associateManufacturedEngineWithVehileOrder = associateManufacturedEngineWithVehileOrder
                ?? throw new ArgumentNullException(nameof(associateManufacturedEngineWithVehileOrder));
        }

        public async Task Consume(ConsumeContext<OptionPackManufactured> context)
        {
            await associateManufacturedEngineWithVehileOrder.Execute(context.Message.VehicleOrderId, context.Message.OptionPackId);
        }
    }
}
