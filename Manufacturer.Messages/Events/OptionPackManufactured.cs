namespace Manufacturer.Messages.Events
{
    public record OptionPackManufactured
    {
        public Guid VehicleOrderId { get; init; }
        public Guid OptionPackId { get; init; }
    }
}
