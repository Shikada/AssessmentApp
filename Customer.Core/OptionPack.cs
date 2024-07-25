using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Core
{
    public class OptionPack
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int AvailableQuantity { get; private set; }
        public int ReservedQuantity { get; private set; }
        public decimal Price { get; private set; }

        // warning disabled because this constructor is used only by EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OptionPack() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public OptionPack(string name, int availableQuantity, int reservedQuantity, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
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
