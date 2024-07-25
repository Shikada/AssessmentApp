using Manufacturer.Application.Ports;
using Manufacturer.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Manufacturer.Application.UseCases
{
    public class CompleteOptionPackManufacture
    {
        private readonly ILogger<CompleteOptionPackManufacture> logger;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IManufactureItemRepo manufactureItemRepo;

        public CompleteOptionPackManufacture(
            ILogger<CompleteOptionPackManufacture> logger,
            IPublishEndpoint publishEndpoint,
            IManufactureItemRepo manufactureItemRepo)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            this.manufactureItemRepo = manufactureItemRepo ?? throw new ArgumentNullException(nameof(manufactureItemRepo));
        }
        public async Task Execute(Guid optionPackManufactureItemId)
        {
            var manufactureItem = await manufactureItemRepo.GetOptionPackItem(optionPackManufactureItemId);
            manufactureItem!.CompleteManufacture();

            await publishEndpoint.Publish(new OptionPackManufactured
            {
                VehicleOrderId = manufactureItem.VehicleOrderId,
                OptionPackId = manufactureItem.OptionPackId
            });

            await manufactureItemRepo.Save(manufactureItem);
        }

    }
}
