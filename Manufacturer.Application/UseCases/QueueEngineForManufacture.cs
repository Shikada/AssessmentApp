using Manufacturer.Application.Ports;
using Manufacturer.Core;
using Microsoft.Extensions.Logging;

namespace Manufacturer.Application.UseCases
{
    public class QueueEngineForManufacture
    {
        private readonly ILogger<QueueEngineForManufacture> logger;
        private readonly IManufactureItemRepo manufactureItemRepo;

        public QueueEngineForManufacture(ILogger<QueueEngineForManufacture> logger, IManufactureItemRepo manufactureItemRepo)
        {
            this.logger = logger;
            this.manufactureItemRepo = manufactureItemRepo;
        }

        public async Task Execute(Guid engineId, Guid vehicleOrderId)
        {
            var manufactureItem = new EngineManufactureItem(Guid.NewGuid(), engineId, vehicleOrderId);
            await manufactureItemRepo.AddEngine(manufactureItem);
        }
    }
}
