namespace Manufacturer.Core
{
    public class OptionPack
    {
        public Guid Id { get; private set; }
        public string SerialNumber { get; private set; }
        public string Name { get; private set; }

        public OptionPack(Guid id, string serialNumber, string name)
        {
            Id = id;
            SerialNumber = serialNumber;
            Name = name;
        }
    }
}
