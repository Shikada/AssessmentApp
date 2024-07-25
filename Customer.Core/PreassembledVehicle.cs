using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Customer.Core
{
    public class PreassembledVehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; private set; }
        public Guid EngineId { get; private set; }
        public Guid ChassisId { get; private set; }
        public Guid OptionPackId { get; private set; }
        public int AvailableQuantity { get; private set; }
        public int ReservedQuantity { get; private set; }
        public decimal Price { get; private set; }

        // warning disabled because this constructor is used only by EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PreassembledVehicle() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public PreassembledVehicle(
            Guid engineId,
            Guid chassis,
            Guid optionPack,
            int availableQuantity,
            int reservedQuantity,
            decimal price)
        {
            Id = Guid.NewGuid();
            EngineId = engineId;
            ChassisId = chassis;
            OptionPackId = optionPack;
            AvailableQuantity = availableQuantity > 0 ? availableQuantity : 0;
            ReservedQuantity = reservedQuantity > 0 ? reservedQuantity : 0;

            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price));

            Price = price;
            Price = price;
        }

        public bool Reserve()
        {
            if (AvailableQuantity <= 0)
                return false;

            AvailableQuantity--;
            ReservedQuantity++;

            return true;
        }

        public bool Order()
        {
            if (ReservedQuantity <= 0)
                return false;

            ReservedQuantity--;

            return true;
        }
    }
}
