using Manufacturer.Application.UseCases;
using Manufacturer.Messages.Commands;
using MassTransit;

namespace Manufacturer.Application.Consumers
{
    public class ManufactureChassisConsumer : IConsumer<ManufactureChassis>
    {
        private readonly QueueChassisForManufacture queueChassisForManufacture;

        public ManufactureChassisConsumer(QueueChassisForManufacture queueChassisForManufacture)
        {
            this.queueChassisForManufacture = queueChassisForManufacture;
        }

        public async Task Consume(ConsumeContext<ManufactureChassis> context)
        {
            await queueChassisForManufacture.Execute(context.Message.ChassisId, context.Message.VehicleOrderId);
        }
    }
}
