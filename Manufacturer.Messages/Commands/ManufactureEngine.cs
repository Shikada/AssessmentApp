namespace Manufacturer.Messages.Commands
{
    public record ManufactureEngine
    {
        public Guid VehicleOrderId { get; init; }
        public Guid EngineId { get; init; }
    }
}
