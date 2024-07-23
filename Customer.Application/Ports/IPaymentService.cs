﻿using Customer.Core;

namespace Customer.Application.Ports
{
    public interface IPaymentService
    {
        Task<Invoice?> CreateNewInvoice(VehicleOrder vehicleOrder);
    }
}