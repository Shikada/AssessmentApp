using Customer.Core;

namespace Customer.Application.Ports
{
    public interface IWarehouseRepository
    {
        Task<Warehouse?> GetWarehouse(Guid id);
        Task<Warehouse?> Save(Warehouse warehouse);
    }
}
