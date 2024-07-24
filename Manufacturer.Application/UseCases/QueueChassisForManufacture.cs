using Manufacturer.Application.Ports;
using Manufacturer.Core;
using Microsoft.Extensions.Logging;

namespace Manufacturer.Application.UseCases
{
    public  class QueueChassisForManufacture
    {
        private readonly ILogger<QueueEngineForManufacture> logger;
        private readonly IManufactureItemRepo manufactureItemRepo;

        public QueueChassisForManufacture(ILogger<QueueEngineForManufacture> logger, IManufactureItemRepo manufactureItemRepo)
        {
            this.logger = logger;
            this.manufactureItemRepo = manufactureItemRepo;
        }

        public async Task Execute(Guid chassisId, Guid vehicleOrderId)
        {
            var manufactureItem = new ChassisManufactureItem(Guid.NewGuid(), chassisId, vehicleOrderId);
            await manufactureItemRepo.AddChassis(manufactureItem);
        }
    }
}
