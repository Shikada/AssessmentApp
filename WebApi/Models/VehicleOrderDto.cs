using Customer.Core;

namespace WebApi.Models
{
    public record VehicleOrderDto
    {
        public Guid Id { get; init; }
        public string Status { get; init; }
        public Guid CustomerId { get; init; }
        public Guid EngineId { get; init; }
        public Guid ChassisId { get; init; }
        public Guid OptionPackId { get; init; }
        public DateTime Timestamp { get; init; }
        public InvoiceDto? Invoice { get; init; }
        public Guid? PreassembledVehicleId { get; init; }
        public List<Guid> PartsAwaitingManufacture { get; init; }

        public VehicleOrderDto(VehicleOrder vehicleOrder)
        {
            Id = vehicleOrder.Id;
            Status = VehicleOrderStatusToString(vehicleOrder.Status);
            CustomerId = vehicleOrder.CustomerId;
            EngineId = vehicleOrder.EngineId;
            ChassisId = vehicleOrder.ChassisId;
            OptionPackId = vehicleOrder.OptionPackId;
            Timestamp = vehicleOrder.Timestamp;
            Invoice = vehicleOrder.Invoice is null ? null : new InvoiceDto(vehicleOrder.Invoice);
            PartsAwaitingManufacture = vehicleOrder.PartsAwaitingManufacture;
        }

        private string VehicleOrderStatusToString(VehicleOrderStatus status)
        {
            switch (status)
            {
                case VehicleOrderStatus.Initial:
                    return "Initial";
                case VehicleOrderStatus.AwaitingPayment:
                    return "AwaitingPayment";
                case VehicleOrderStatus.Accepted:
                    return "Accepted";
                case VehicleOrderStatus.WaitingForParts:
                    return "WaitingForParts";
                case VehicleOrderStatus.AllPartsReady:
                    return "AllPartsReady";
                case VehicleOrderStatus.Cancelled:
                    return "Cancelled";
                default:
                    return "Unknown";
            }
        }
    }
}
