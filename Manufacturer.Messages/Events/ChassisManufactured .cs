namespace Manufacturer.Messages.Events
{
    public record ChassisManufactured
    {
        public Guid VehicleOrderId { get; init; }
        public Guid ChassisId { get; init; }
    }
}
