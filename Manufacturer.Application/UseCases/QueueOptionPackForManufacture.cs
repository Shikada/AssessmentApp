using Manufacturer.Application.Ports;
using Manufacturer.Core;
using Microsoft.Extensions.Logging;

namespace Manufacturer.Application.UseCases
{
    public class QueueOptionPackForManufacture
    {
        private readonly ILogger<QueueEngineForManufacture> logger;
        private readonly IManufactureItemRepo manufactureItemRepo;

        public QueueOptionPackForManufacture(ILogger<QueueEngineForManufacture> logger, IManufactureItemRepo manufactureItemRepo)
        {
            this.logger = logger;
            this.manufactureItemRepo = manufactureItemRepo;
        }

        public async Task Execute(Guid optionPackId, Guid vehicleOrderId)
        {
            var manufactureItem = new OptionPackManufactureItem(Guid.NewGuid(), optionPackId, vehicleOrderId);
            await manufactureItemRepo.AddOptionsPack(manufactureItem);
        }
    }
}
