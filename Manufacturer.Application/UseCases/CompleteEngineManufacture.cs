using Manufacturer.Application.Ports;
using Manufacturer.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Manufacturer.Application.UseCases
{
    public class CompleteEngineManufacture
    {
        private readonly ILogger<CompleteEngineManufacture> logger;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IManufactureItemRepo manufactureItemRepo;

        public CompleteEngineManufacture(
            ILogger<CompleteEngineManufacture> logger,
            IPublishEndpoint publishEndpoint,
            IManufactureItemRepo manufactureItemRepo)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            this.manufactureItemRepo = manufactureItemRepo ?? throw new ArgumentNullException(nameof(manufactureItemRepo));
        }

        public async Task Execute(Guid engineManufactureItemId)
        {
            var manufactureItem = await manufactureItemRepo.GetEngineItem(engineManufactureItemId);
            manufactureItem!.CompleteManufacture();

            await publishEndpoint.Publish(new EngineManufactured
            {
                VehicleOrderId = manufactureItem.VehicleOrderId,
                EngineId = manufactureItem.EngineId
            });

            await manufactureItemRepo.Save(manufactureItem);
        }
    }
}
