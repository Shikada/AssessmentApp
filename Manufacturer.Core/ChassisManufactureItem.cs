namespace Manufacturer.Core
{
    public class ChassisManufactureItem
    {
        public Guid Id { get; private set; }
        public Guid ChassisId { get; private set; }
        public Guid VehicleOrderId { get; private set; }
        public ManufactureItemStatus Status { get; private set; }

        public ChassisManufactureItem(Guid id, Guid chassisId, Guid vehicleOrderId)
        {
            Id = id;
            ChassisId = chassisId;
            VehicleOrderId = vehicleOrderId;
            Status = ManufactureItemStatus.Queued;
        }
    }
}
