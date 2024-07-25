using Manufacturer.Application.Ports;
using Manufacturer.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Manufacturer.Application.UseCases
{
    public class CompleteChassisManufacture
    {
        private readonly ILogger<CompleteChassisManufacture> logger;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IManufactureItemRepo manufactureItemRepo;

        public CompleteChassisManufacture(
            ILogger<CompleteChassisManufacture> logger,
            IPublishEndpoint publishEndpoint,
            IManufactureItemRepo manufactureItemRepo)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            this.manufactureItemRepo = manufactureItemRepo ?? throw new ArgumentNullException(nameof(manufactureItemRepo));
        }

        public async Task Execute(Guid chassisManufactureItemId)
        {
            var manufactureItem = await manufactureItemRepo.GetChassisItem(chassisManufactureItemId);
            manufactureItem!.CompleteManufacture();

            await publishEndpoint.Publish(new ChassisManufactured
            {
                VehicleOrderId = manufactureItem.VehicleOrderId,
                ChassisId = manufactureItem.ChassisId
            });

            await manufactureItemRepo.Save(manufactureItem);
        }
    }
}
