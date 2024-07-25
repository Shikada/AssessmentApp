namespace Manufacturer.Core
{
    public class OptionPackManufactureItem
    {
        public Guid Id { get; private set; }
        public Guid OptionPackId { get; private set; }
        public Guid VehicleOrderId { get; private set; }
        public ManufactureItemStatus Status { get; private set; }

        public OptionPackManufactureItem(Guid id, Guid optionPackId, Guid vehicleOrderId)
        {
            Id = id;
            OptionPackId = optionPackId;
            VehicleOrderId = vehicleOrderId;
            Status = ManufactureItemStatus.Queued;
        }

        public void CompleteManufacture()
        {
            Status = ManufactureItemStatus.ProductionDone;
        }
    }
}
