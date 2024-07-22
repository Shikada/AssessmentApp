namespace WebApi.Models
{
    public record CreateVehicleOrderDto
    {
        public required Guid CustomerId { get; init; }
        public required Guid EngineId { get; init; }
        public required Guid ChassisId { get; init; }
        public required Guid OptionPackId { get; init; }
    }
}
