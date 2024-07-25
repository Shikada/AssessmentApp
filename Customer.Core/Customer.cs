using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Core
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }

        public Customer(string firstName, string lastName, string address)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }
    }
}
