using Manufacturer.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private readonly ILogger<VehicleOrderController> logger;
        private readonly CompleteEngineManufacture completeEngineManufacture;
        private readonly CompleteChassisManufacture completeChassisManufacture;
        private readonly CompleteOptionPackManufacture completeOptionPackManufacture;

        public ManufacturerController(
            ILogger<VehicleOrderController> logger,
            CompleteEngineManufacture completeEngineManufacture,
            CompleteChassisManufacture completeChassisManufacture,
            CompleteOptionPackManufacture completeOptionPackManufacture)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.completeEngineManufacture = completeEngineManufacture ?? throw new ArgumentNullException(nameof(completeEngineManufacture));
            this.completeChassisManufacture = completeChassisManufacture ?? throw new ArgumentNullException(nameof(completeChassisManufacture));
            this.completeOptionPackManufacture = completeOptionPackManufacture ?? throw new ArgumentNullException(nameof(completeOptionPackManufacture));
        }

        [HttpPost("engine")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> EngineManufactured(Guid engineManufactureItemId)
        {
            await completeEngineManufacture.Execute(engineManufactureItemId);

            return Ok();
        }

        [HttpPost("chassis")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ChassisManufactured(Guid chassisManufactureItemId)
        {
            await completeChassisManufacture.Execute(chassisManufactureItemId);

            return Ok();
        }

        [HttpPost("option-pack")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> OptionPackManufactured(Guid optionPackManufactureItemId)
        {
            await completeOptionPackManufacture.Execute(optionPackManufactureItemId);

            return Ok();
        }
    }
}
