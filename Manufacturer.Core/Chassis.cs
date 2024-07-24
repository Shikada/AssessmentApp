namespace Manufacturer.Core
{
    public class Chassis
    {
        public Guid Id { get; private set; }
        public string SerialNumber { get; private set; }
        public string Model { get; private set; }
        public string Type { get; private set; }

        public Chassis(Guid id, string serialNumber, string model, string type)
        {
            Id = id;
            SerialNumber = serialNumber;
            Model = model;
            Type = type;
        }
    }
}
