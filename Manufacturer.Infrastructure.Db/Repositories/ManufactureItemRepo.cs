using Manufacturer.Application.Ports;
using Manufacturer.Core;

namespace Manufacturer.Infrastructure.Db.Repositories
{
    public class ManufactureItemRepo : IManufactureItemRepo
    {
        public Task AddEngine(EngineManufactureItem engineManufactureItem)
        {
            throw new NotImplementedException();
        }

        public Task AddChassis(ChassisManufactureItem chassisManufactureItem)
        {
            throw new NotImplementedException();
        }

        public Task AddOptionsPack(OptionPackManufactureItem optionPackManufactureItem)
        {
            throw new NotImplementedException();
        }

        public Task<EngineManufactureItem> GetEngineItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ChassisManufactureItem> GetChassisItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OptionPackManufactureItem> GetOptionPackItem(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
