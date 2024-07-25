﻿namespace Customer.Core
{
    public enum VehicleOrderStatus
    {
        Initial = 0,
        AwaitingPayment,
        Accepted,
        WaitingForParts,
        AllPartsReady,
        Fulfilled,
        Cancelled
    }
}
