namespace Customer.Messages.Commands
{
    public record CreateVehicleOrder
    {
        public required Guid CustomerId { get; init; }
        public required Guid EngineId { get; init; }
        public required Guid ChassisId { get; init; }
        public required Guid OptionPackId { get; init; }
    }
}
