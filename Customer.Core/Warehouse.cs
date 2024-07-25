namespace Customer.Core
{
    /// <summary>
    /// Aggregate root for the entire warehouse inventory.
    /// </summary>
    public class Warehouse
    {
        public static readonly Guid MainWarehouseId = Guid.NewGuid();
        public Guid Id { get; private set; }
        public List<Engine> Engines { get; private set; }
        public List<Chassis> AllChassis { get; private set; }
        public List<OptionPack> OptionPacks { get; private set; }
        public List<PreassembledVehicle> PreassembledVehicles { get; private set; }

        // warning disabled because this constructor is used only by EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Warehouse() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Warehouse(
            List<Engine> engines,
            List<Chassis> allChassis,
            List<OptionPack> optionPacks,
            List<PreassembledVehicle> preassembledVehicles)
        {
            Id = MainWarehouseId;
            Engines = engines ?? throw new ArgumentNullException(nameof(engines));
            AllChassis = allChassis ?? throw new ArgumentNullException(nameof(allChassis));
            OptionPacks = optionPacks ?? throw new ArgumentNullException(nameof(optionPacks));
            PreassembledVehicles = preassembledVehicles ?? throw new ArgumentNullException(nameof(preassembledVehicles));
        }

        public List<PreassembledVehicle> FindMatchingPreassemlbedVehicles(
            Guid engineId,
            Guid chassisId,
            Guid optionPackId)
        {
            return PreassembledVehicles
                .Where(vehicle =>
                    vehicle.EngineId == engineId
                    && vehicle.ChassisId == chassisId
                    && vehicle.OptionPackId == optionPackId)
                .ToList();
        }

        public bool ReservePreassembledVehicle(Guid vehicleId)
        {
            var vehicle = PreassembledVehicles.FirstOrDefault(x => x.Id == vehicleId);

            if (vehicle == null)
                return false;

            vehicle.Reserve();

            return true;

        }

        public UnavailableVehicleOrderParts ReserveParts(VehicleOrder vehicleOrder)
        {
            //TODO: add error handling here for all First() calls
            var engine = Engines.First(x => x.Id == vehicleOrder.EngineId);
            var chassis = AllChassis.First(x => x.Id == vehicleOrder.ChassisId);
            var optionPack = OptionPacks.First(x => x.Id == vehicleOrder.OptionPackId);

            var isEngineAvailable = engine.Reserve();
            var isChassisAvailable = chassis.Reserve();
            var isOptionPackAvailable = optionPack.Reserve();

            return new UnavailableVehicleOrderParts
            {
                EngineId = isEngineAvailable ? null : engine.Id,
                ChassisId = isChassisAvailable? null : chassis.Id,
                OptionPackId = isOptionPackAvailable ? null : optionPack.Id,
            };
        }

        public bool AcceptOrder(VehicleOrder vehicleOrder)
        {
            if (vehicleOrder.PreassembledVehicleId is not null)
            {
                var vehicle = PreassembledVehicles.FirstOrDefault(x => x.Id == vehicleOrder.PreassembledVehicleId);

                if (vehicle == null)
                    return false;

                vehicle.Order();
            }
            else
            {
                //TODO: add error handling here for all First() calls
                var engine = Engines.First(x => x.Id == vehicleOrder.EngineId);
                var chassis = AllChassis.First(x => x.Id == vehicleOrder.ChassisId);
                var optionPack = OptionPacks.First(x => x.Id == vehicleOrder.OptionPackId);

                engine.Order();
                chassis.Order();
                optionPack.Order();
            }

            return true;

        }

        public decimal GetPrice(VehicleOrder vehicleOrder)
        {
            //TODO: add error handling here for all First() calls
            if (vehicleOrder.PreassembledVehicleId != null)
                return PreassembledVehicles.First(x => x.Id == vehicleOrder.PreassembledVehicleId).Price;

            var engine = Engines.First(x => x.Id == vehicleOrder.EngineId);
            var chassis = AllChassis.First(x => x.Id == vehicleOrder.ChassisId);
            var optionPack = OptionPacks.First(x => x.Id == vehicleOrder.OptionPackId);

            return engine.Price + chassis.Price + optionPack.Price;
        }
    }
}
