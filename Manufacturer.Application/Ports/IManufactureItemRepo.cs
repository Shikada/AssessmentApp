using Manufacturer.Core;

namespace Manufacturer.Application.Ports
{
    public interface IManufactureItemRepo
    {
        Task AddEngine(EngineManufactureItem engineManufactureItem);
        Task AddChassis(ChassisManufactureItem chassisManufactureItem);
        Task AddOptionsPack(OptionPackManufactureItem optionPackManufactureItem);
    }
}
