namespace Customer.Core
{
    public class Chassis
    {
        public Guid Id { get; private set; }
        public string Model { get; private set; }
        public string Type { get; private set; }

        // warning disabled because this constructor is used only by EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Chassis() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Chassis(string model, string type)
        {
            Id = Guid.NewGuid();
            Model = model ?? throw new ArgumentNullException(nameof(model));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}
