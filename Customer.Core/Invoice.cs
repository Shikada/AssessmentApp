namespace Customer.Core
{
    public class Invoice
    {
        public Guid Id { get; private set; }
        public Guid VehicleOrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal Price { get; private set; }
        public bool Paid { get; private set; }

        // warning disabled because this constructor is used only by EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Invoice() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Invoice(Guid id, VehicleOrder vehicleOrder, decimal price)
        {
            Id = id;
            VehicleOrderId = vehicleOrder.Id;
            CustomerId = vehicleOrder.CustomerId;
            Price = price;
            Paid = false;
        }

        public void Pay()
        {
            Paid = true;
        }
    }
}
