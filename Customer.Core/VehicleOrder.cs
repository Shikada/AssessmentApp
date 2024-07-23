namespace Customer.Core
{
    public class VehicleOrder
    {
        public Guid Id { get; private set; }
        public VehicleOrderStatus Status { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid EngineId { get; private set; }
        public Guid ChassisId { get; private set; }
        public Guid OptionPackId { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Invoice? Invoice { get; private set; }
        public Guid? PreassembledVehicleId { get; private set; }

        // warning disabled because this constructor is used only by EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public VehicleOrder() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public VehicleOrder(
            Guid customerId,
            Guid engineId,
            Guid chassisId,
            Guid optionPackId)
        {
            Id = Guid.NewGuid();
            Status = VehicleOrderStatus.Initial;
            CustomerId = customerId;
            EngineId = engineId;
            ChassisId = chassisId;
            OptionPackId = optionPackId;
            Timestamp = DateTime.UtcNow;
        }

        public void AssociatePreassembledVehicle(Guid preassembledVehicleId)
        {
            PreassembledVehicleId = preassembledVehicleId;
        }

        public void AwaitPayment(Invoice invoice)
        {
            Status = VehicleOrderStatus.AwaitingPayment;
            Invoice = invoice;
        }

        public void Accept()
        {
            Status = VehicleOrderStatus.Accepted;
        }
    }
}
