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
        private readonly ReservePreassembledVehicleForPayment orderPreassembledVehicle;

        public VehicleOrderController(
            ILogger<VehicleOrderController> logger,
            CreateVehicleOrder createVehicleOrderUseCase,
            GetMatchingPreassemlbedVehiclesForOrder getMatchingPreassemlbedVehiclesForOrder,
            ReservePreassembledVehicleForPayment orderPreassembledVehicle)
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PreassembledVehicle>>> GetMatchingPreassembledVehicles([FromQuery] Guid vehicleOrderId)
        {
            try
            {
                return await getMatchingPreassemlbedVehiclesForOrder.ExecuteAsync(vehicleOrderId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("order-vehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> OrderPreassembledVehicle([FromQuery] Guid vehicleOrderId, [FromQuery] Guid vehicleId)
        {
            try
            {
                await orderPreassembledVehicle.Execute(vehicleOrderId, vehicleId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok();
        }
    }
}
