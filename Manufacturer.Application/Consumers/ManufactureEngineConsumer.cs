using Manufacturer.Application.UseCases;
using Manufacturer.Messages.Commands;
using MassTransit;

namespace Manufacturer.Application.Consumers
{
    public class ManufactureEngineConsumer : IConsumer<ManufactureEngine>
    {
        private readonly QueueEngineForManufacture queueEngineForManufacture;

        public ManufactureEngineConsumer(QueueEngineForManufacture queueEngineForManufacture)
        {
            this.queueEngineForManufacture = queueEngineForManufacture;
        }

        public async Task Consume(ConsumeContext<ManufactureEngine> context)
        {
            await queueEngineForManufacture.Execute(context.Message.EngineId, context.Message.VehicleOrderId);
        }
    }
}
