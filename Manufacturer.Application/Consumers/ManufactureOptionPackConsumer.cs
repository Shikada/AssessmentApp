using Manufacturer.Application.UseCases;
using Manufacturer.Messages.Commands;
using MassTransit;

namespace Manufacturer.Application.Consumers
{
    public class ManufactureOptionPackConsumer : IConsumer<ManufactureOptionPack>
    {
        private readonly QueueOptionPackForManufacture queueOptionPackForManufacture;

        public ManufactureOptionPackConsumer(QueueOptionPackForManufacture queueOptionPackForManufacture)
        {
            this.queueOptionPackForManufacture = queueOptionPackForManufacture;
        }

        public async Task Consume(ConsumeContext<ManufactureOptionPack> context)
        {
            await queueOptionPackForManufacture.Execute(context.Message.OptionPackId, context.Message.VehicleOrderId);
        }
    }
}
