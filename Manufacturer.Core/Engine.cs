namespace Manufacturer.Core
{
    public class Engine
    {
        public Guid Id { get; private set; }
        public string SerialNumber { get; private set; }
        public string Model { get; private set; }
        public string Type { get; private set; }
        public int Killowatts { get; private set; }

        public Engine(Guid id, string serialNumber, string model, string type, int killowatts)
        {
            Id = id;
            SerialNumber = serialNumber;
            Model = model;
            Type = type;
            Killowatts = killowatts;
        }
    }
}
