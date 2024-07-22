using Customer.Core;

namespace Customer.Application.Ports
{
    public interface IWarehouseRepository
    {
        Task<Warehouse> GetWarehouse(Guid id);
    }
}
