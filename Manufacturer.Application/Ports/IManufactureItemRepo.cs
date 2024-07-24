using Manufacturer.Core;

namespace Manufacturer.Application.Ports
{
    public interface IManufactureItemRepo
    {
        Task AddEngine(EngineManufactureItem engineManufactureItem);
        Task AddChassis(ChassisManufactureItem chassisManufactureItem);
        Task AddOptionsPack(OptionPackManufactureItem optionPackManufactureItem);
        Task<EngineManufactureItem> GetEngineItem(Guid id);
        Task<ChassisManufactureItem> GetChassisItem(Guid id);
        Task<OptionPackManufactureItem> GetOptionPackItem(Guid id);
    }
}
