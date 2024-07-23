using System.Diagnostics;

namespace Customer.Core
{
    public class Engine
    {
        public Guid Id { get; private set; }
        public string Model { get; private set; }
        public string Type { get; private set; }
        public int Killowatts { get; private set; }
        public int AvailableQuantity { get; private set; }
        public int ReservedQuantity { get; private set; }
        public decimal Price { get; private set; }

        // warning disabled because this constructor is used only by EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Engine() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Engine(
            string model,
            string type,
            int killowatts,
            int availableQuantity,
            int reservedQuantity,
            decimal price)
        {
            Id = Guid.NewGuid();
            Model = model ?? throw new ArgumentNullException(nameof(model));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Killowatts = killowatts;
            AvailableQuantity = availableQuantity > 0 ? availableQuantity : 0;
            ReservedQuantity = reservedQuantity > 0 ? reservedQuantity : 0;

            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price));

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
