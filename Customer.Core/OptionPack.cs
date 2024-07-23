using System.Diagnostics;

namespace Customer.Core
{
    public class OptionPack
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        // warning disabled because this constructor is used only by EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OptionPack() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public OptionPack(string name, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));

            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price));

            Price = price;
            Price = price;
        }
    }
}
