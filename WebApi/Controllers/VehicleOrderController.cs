using Customer.Application.UseCases;
using Customer.Core;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleOrderController : ControllerBase
    {
        private readonly ILogger<VehicleOrderController> logger;
        private readonly CreateVehicleOrder createVehicleOrderUseCase;
        private readonly GetMatchingPreassemlbedVehiclesForOrder getMatchingPreassemlbedVehiclesForOrder;
        private readonly OrderPreassembledVehicle orderPreassembledVehicle;

        public VehicleOrderController(
            ILogger<VehicleOrderController> logger,
            CreateVehicleOrder createVehicleOrderUseCase,
            GetMatchingPreassemlbedVehiclesForOrder getMatchingPreassemlbedVehiclesForOrder,
            OrderPreassembledVehicle orderPreassembledVehicle)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.createVehicleOrderUseCase = createVehicleOrderUseCase ?? throw new ArgumentNullException(nameof(createVehicleOrderUseCase));
            this.getMatchingPreassemlbedVehiclesForOrder = getMatchingPreassemlbedVehiclesForOrder
                ?? throw new ArgumentNullException(nameof(getMatchingPreassemlbedVehiclesForOrder));
            this.orderPreassembledVehicle = orderPreassembledVehicle ?? throw new ArgumentNullException(nameof(orderPreassembledVehicle));
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateVehicleOrder(CreateVehicleOrderDto request)
        {
            logger.LogInformation("Creating a vehicle order for {customerId}", request.CustomerId);

            await createVehicleOrderUseCase.ExecuteAsync(new Customer.Messages.Commands.CreateVehicleOrder
            {
                CustomerId = request.CustomerId,
                EngineId = request.EngineId,
                ChassisId = request.ChassisId,
                OptionPackId = request.OptionPackId
            });

            return Ok();
        }

        [HttpGet("matching-preassembled")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PreassembledVehicle>>> GetMatchingPreassembledVehicles([FromQuery] Guid vehicleOrderId)
        {
            return await getMatchingPreassemlbedVehiclesForOrder.ExecuteAsync(vehicleOrderId);
        }

        [HttpPost("order-vehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> OrderPreassembledVehicle([FromQuery] Guid vehicleOrderId, [FromQuery] Guid vehicleId)
        {
            await orderPreassembledVehicle.Execute(vehicleOrderId, vehicleId);

            return Ok();
        }
    }
}
