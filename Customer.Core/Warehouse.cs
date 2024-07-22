namespace Customer.Core
{
    /// <summary>
    /// Aggregate root for the entire warehouse inventory.
    /// </summary>
    public class Warehouse
    {
        public static readonly Guid MainWarehouseId = Guid.NewGuid();
        public List<Engine> Engines { get; private set; }
        public List<Chassis> AllChassis { get; private set; }
        public List<OptionPack> OptionPacks { get; private set; }
        public List<PreassembledVehicle> PreassembledVehicles { get; private set; }

        // warning disabled because this constructor is used only by EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Warehouse() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public List<PreassembledVehicle> FindMatchingPreassemlbedVehicles(
            Guid engine,
            Guid chassis,
            Guid optionPack)
        {
            return PreassembledVehicles
                .Where(vehicle =>
                    vehicle.Engine.Id == engine
                    && vehicle.Chassis.Id == chassis
                    && vehicle.OptionPack.Id == optionPack)
                .ToList();
        }

        public bool OrderPreassembledVehicle(Guid vehicleId)
        {
            var vehicle = PreassembledVehicles.FirstOrDefault(x => x.Id == vehicleId);

            if (vehicle == null)
                return false;

            vehicle.DecrementAvailableQuantity();

            return true;

        }
    }
}
