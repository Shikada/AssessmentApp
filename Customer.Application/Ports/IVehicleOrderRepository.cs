using Customer.Core;

namespace Customer.Application.Ports
{
    public interface IVehicleOrderRepository
    {
        Task<VehicleOrder?> GetById(Guid id);
        Task<VehicleOrder?> GetByInvoiceId(Guid invoiceId);
        Task<VehicleOrder?> Save(VehicleOrder vehicleOrder);
    }
}
