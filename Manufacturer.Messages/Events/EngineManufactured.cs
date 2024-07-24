namespace Manufacturer.Messages.Events
{
    public record EngineManufactured
    {
        public Guid VehicleOrderId { get; init; }
        public Guid EngineId { get; init; }
    }
}
