﻿using Customer.Core;

namespace Customer.Application.Ports
{
    public interface IVehicleOrderRepository
    {
        Task<VehicleOrder?> GetVehicleOrder(Guid id);
        Task<VehicleOrder?> Save(VehicleOrder vehicleOrder);
    }
}
