namespace Customer.Core
{
    public class PreassembledVehicle
    {
        public Guid Id { get; private set; }
        public Engine Engine { get; private set; }
        public Chassis Chassis { get; private set; }
        public OptionPack OptionPack { get; private set; }
        public int AvailableQuantity { get; private set; }

        // warning disabled because this constructor is used only by EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PreassembledVehicle() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public PreassembledVehicle(Guid id, Engine engine, Chassis chassis, OptionPack optionPack, int availableQuantity)
        {
            Id = id;
            Engine = engine ?? throw new ArgumentNullException(nameof(engine));
            Chassis = chassis ?? throw new ArgumentNullException(nameof(chassis));
            OptionPack = optionPack ?? throw new ArgumentNullException(nameof(optionPack));
            AvailableQuantity = availableQuantity > 0 ? availableQuantity : 0;
        }

        public bool DecrementAvailableQuantity()
        {
            if (AvailableQuantity <= 0)
                return false;

            AvailableQuantity--;

            return true;
        }
    }
}
