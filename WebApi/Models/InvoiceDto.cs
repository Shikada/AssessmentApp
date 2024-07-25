using Customer.Core;

namespace WebApi.Models
{
    public record InvoiceDto
    {
        public Guid Id { get; init; }
        public decimal Price { get; init; }
        public bool Paid { get; init; }

        public InvoiceDto(Invoice invoice)
        {
            Id = invoice.Id;
            Price = invoice.Price;
            Paid = invoice.Paid;
        }
    }
}
