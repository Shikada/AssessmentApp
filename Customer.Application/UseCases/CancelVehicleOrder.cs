using Customer.Application.Ports;
using Microsoft.Extensions.Logging;

namespace Customer.Application.UseCases
{
    public class CancelVehicleOrder
    {
        private readonly ILogger<CancelVehicleOrder> logger;
        private readonly IVehicleOrderRepository vehicleOrderRepository;
        private readonly IPaymentService paymentService;

        public CancelVehicleOrder(
            ILogger<CancelVehicleOrder> logger,
            IVehicleOrderRepository vehicleOrderRepository,
            IPaymentService paymentService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.vehicleOrderRepository = vehicleOrderRepository ?? throw new ArgumentNullException(nameof(vehicleOrderRepository));
            this.paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        }

        public async Task Execute(Guid vehicleOrderId)
        {
            var vehicleOrder = await vehicleOrderRepository.GetById(vehicleOrderId);

            if (vehicleOrder is null)
            {
                logger.LogError("Could not get a vehicle order with ID {vehicleOrderId} from database that should exist", vehicleOrderId);
                throw new Exception($"Could not get a vehicle order with ID {vehicleOrderId} from database that should exist");
            }

            vehicleOrder.Cancel();

            if (vehicleOrder.Invoice is not null)
                await paymentService.ProcessInvoiceCancelled(vehicleOrder.Invoice);

            await vehicleOrderRepository.Save(vehicleOrder);
        }
    }
}
