namespace Customer.Messages.Events
{
    public record VehicleOrderCreated
    {
        public required Guid VehicleOrderId { get; init; }
        public required Guid CustomerId { get; init; }
    }
}
