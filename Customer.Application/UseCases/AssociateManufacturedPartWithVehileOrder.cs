using Customer.Application.Ports;
using Microsoft.Extensions.Logging;

namespace Customer.Application.UseCases
{
    public class AssociateManufacturedPartWithVehileOrder
    {
        private readonly ILogger<AssociateManufacturedPartWithVehileOrder> logger;
        private readonly IVehicleOrderRepository vehicleOrderRepository;

        public AssociateManufacturedPartWithVehileOrder(
            ILogger<AssociateManufacturedPartWithVehileOrder> logger,
            IVehicleOrderRepository vehicleOrderRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.vehicleOrderRepository = vehicleOrderRepository ?? throw new ArgumentNullException(nameof(vehicleOrderRepository));
        }

        public async Task Execute(Guid vehicleOrderId, Guid partId)
        {
            var vehicleOrder = await vehicleOrderRepository.GetById(vehicleOrderId);

            if (vehicleOrder is null)
            {
                logger.LogError("Could not get a vehicle order with ID {vehicleOrderId} from database that should exist", vehicleOrderId);
                throw new Exception($"Could not get a vehicle order with ID {vehicleOrderId} from database that should exist");
            }

            vehicleOrder.StopWaitingForPart(partId);

            if (vehicleOrder.Status == Core.VehicleOrderStatus.AllPartsReady)
            {
                vehicleOrder.Fullfil();
            }

            await vehicleOrderRepository.Save(vehicleOrder);
        }
    }
}
