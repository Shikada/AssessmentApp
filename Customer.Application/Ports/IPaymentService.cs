using Customer.Core;

namespace Customer.Application.Ports
{
    public interface IPaymentService
    {
        Task<Invoice?> CreateNewInvoice(VehicleOrder vehicleOrder);
        Task ProcessPayedInvoice(Invoice invoice);
        Task ProcessInvoiceCancelled(Invoice invoice);
    }
}
