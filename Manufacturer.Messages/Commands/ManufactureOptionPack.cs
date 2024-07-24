namespace Manufacturer.Messages.Commands
{
    public record ManufactureOptionPack
    {
        public Guid VehicleOrderId { get; init; }
        public Guid OptionPackId { get; init; }
    }
}
