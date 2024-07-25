using Manufacturer.Application.Ports;
using Manufacturer.Core;

namespace Manufacturer.Infrastructure.Db.Repositories
{
    public class ManufactureItemRepo : IManufactureItemRepo
    {
        private readonly ManufacturerDbContext manufacturerDbContext;

        public ManufactureItemRepo(ManufacturerDbContext manufacturerDbContext)
        {
            this.manufacturerDbContext = manufacturerDbContext;
        }

        public async Task AddEngine(EngineManufactureItem engineManufactureItem)
        {
            manufacturerDbContext.EngineManufactureItems.Add(engineManufactureItem);
            await manufacturerDbContext.SaveChangesAsync();
        }

        public async Task AddChassis(ChassisManufactureItem chassisManufactureItem)
        {
            manufacturerDbContext.ChassisManufactureItems.Add(chassisManufactureItem);
            await manufacturerDbContext.SaveChangesAsync();
        }

        public async Task AddOptionsPack(OptionPackManufactureItem optionPackManufactureItem)
        {
            manufacturerDbContext.OptionPackManufactureItems.Add(optionPackManufactureItem);
            await manufacturerDbContext.SaveChangesAsync();
        }

        public async Task<EngineManufactureItem?> Save(EngineManufactureItem manufactureItem)
        {
            var persistedManufatureItem = await manufacturerDbContext.EngineManufactureItems.FindAsync(manufactureItem.Id);

            if (persistedManufatureItem is null)
                manufacturerDbContext.EngineManufactureItems.Add(manufactureItem);

            await manufacturerDbContext.SaveChangesAsync();

            return await manufacturerDbContext.EngineManufactureItems.FindAsync(manufactureItem.Id);
        }

        public async Task<ChassisManufactureItem?> Save(ChassisManufactureItem manufactureItem)
        {
            var persistedManufatureItem = await manufacturerDbContext.ChassisManufactureItems.FindAsync(manufactureItem.Id);

            if (persistedManufatureItem is null)
                manufacturerDbContext.ChassisManufactureItems.Add(manufactureItem);

            await manufacturerDbContext.SaveChangesAsync();

            return await manufacturerDbContext.ChassisManufactureItems.FindAsync(manufactureItem.Id);
        }

        public async Task<OptionPackManufactureItem?> Save(OptionPackManufactureItem manufactureItem)
        {
            var persistedManufatureItem = await manufacturerDbContext.OptionPackManufactureItems.FindAsync(manufactureItem.Id);

            if (persistedManufatureItem is null)
                manufacturerDbContext.OptionPackManufactureItems.Add(manufactureItem);

            await manufacturerDbContext.SaveChangesAsync();

            return await manufacturerDbContext.OptionPackManufactureItems.FindAsync(manufactureItem.Id);
        }

        public async Task<EngineManufactureItem?> GetEngineItem(Guid id)
        {
            return await manufacturerDbContext.EngineManufactureItems.FindAsync(id);
        }

        public async Task<ChassisManufactureItem?> GetChassisItem(Guid id)
        {
            return await manufacturerDbContext.ChassisManufactureItems.FindAsync(id);
        }

        public async Task<OptionPackManufactureItem?> GetOptionPackItem(Guid id)
        {
            return await manufacturerDbContext.OptionPackManufactureItems.FindAsync(id);
        }
    }
}
