namespace Customer.Core
{
    public record UnavailableVehicleOrderParts
    {
        public Guid? EngineId { get; init; }
        public Guid? ChassisId { get; init; }
        public Guid? OptionPackId { get; init; }
    }
}
