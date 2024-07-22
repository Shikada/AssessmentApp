using Customer.Core;

namespace Customer.Application.Ports
{
    public interface IVehicleOrderRepository
    {
        Task<VehicleOrder> GetVehicleOrder(Guid id);
    }
}
