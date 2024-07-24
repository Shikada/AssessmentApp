namespace Manufacturer.Core
{
    public class EngineManufactureItem
    {
        public Guid Id { get; private set; }
        public Guid EngineId { get; private set; }
        public Guid VehicleOrderId { get; private set; }
        public ManufactureItemStatus Status { get; private set; }

        public EngineManufactureItem(Guid id, Guid engineId, Guid vehicleOrderId)
        {
            Id = id;
            EngineId = engineId;
            VehicleOrderId = vehicleOrderId;
            Status = ManufactureItemStatus.Queued;
        }
    }
}
