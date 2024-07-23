﻿using Customer.Application.UseCases;
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
        private readonly ReservePreassembledVehicleForPayment reservePreassembledVehicle;
        private readonly OrderVehicle orderVehicle;

        public VehicleOrderController(
            ILogger<VehicleOrderController> logger,
            CreateVehicleOrder createVehicleOrderUseCase,
            GetMatchingPreassemlbedVehiclesForOrder getMatchingPreassemlbedVehiclesForOrder,
            ReservePreassembledVehicleForPayment orderPreassembledVehicle,
            OrderVehicle orderVehicle)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.createVehicleOrderUseCase = createVehicleOrderUseCase ?? throw new ArgumentNullException(nameof(createVehicleOrderUseCase));
            this.getMatchingPreassemlbedVehiclesForOrder = getMatchingPreassemlbedVehiclesForOrder
                ?? throw new ArgumentNullException(nameof(getMatchingPreassemlbedVehiclesForOrder));
            this.reservePreassembledVehicle = orderPreassembledVehicle ?? throw new ArgumentNullException(nameof(orderPreassembledVehicle));
            this.orderVehicle = orderVehicle ?? throw new ArgumentNullException(nameof(orderVehicle));
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

        [HttpPost("reserve-vehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ReservePreassembledVehicle([FromQuery] Guid vehicleOrderId, [FromQuery] Guid vehicleId)
        {
            try
            {
                var isSuccessful = await reservePreassembledVehicle.Execute(vehicleOrderId, vehicleId);

                if (isSuccessful is null)
                    return StatusCode(StatusCodes.Status400BadRequest, $"Tried to reserve pressembled vehicle with ID '{vehicleId}' that is unavailable");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Callback for an external payment service provider, once a payment for an invoice was successfully made.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        [HttpPost("payment-callback")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PaymentCallback(Guid invoice)
        {
            try
            {
                var isSuccessful = await orderVehicle.Execute(invoice);

                if (!isSuccessful)
                    return StatusCode(StatusCodes.Status400BadRequest, $"Tried to complete payment of invalid invoice");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok();
        }
    }
}
