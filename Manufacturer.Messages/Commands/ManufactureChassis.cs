namespace Manufacturer.Messages.Commands
{
    public record ManufactureChassis
    {
        public Guid VehicleOrderId { get; init; }
        public Guid ChassisId { get; init; }
    }
}
