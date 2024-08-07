﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Core
{
    /// <summary>
    /// Represents all the details of a vehicle order by a customer. Serves as aggregate root for invoices.
    /// </summary>
    public class VehicleOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; private set; }
        public VehicleOrderStatus Status { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid EngineId { get; private set; }
        public Guid ChassisId { get; private set; }
        public Guid OptionPackId { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Invoice? Invoice { get; private set; }
        public Guid? PreassembledVehicleId { get; private set; }
        public List<Guid> PartsAwaitingManufacture { get; private set; }

        // warning disabled because this constructor is used only by EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public VehicleOrder() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public VehicleOrder(
            Guid customerId,
            Guid engineId,
            Guid chassisId,
            Guid optionPackId)
        {
            Id = Guid.NewGuid();
            Status = VehicleOrderStatus.Initial;
            CustomerId = customerId;
            EngineId = engineId;
            ChassisId = chassisId;
            OptionPackId = optionPackId;
            Timestamp = DateTime.UtcNow;
            PartsAwaitingManufacture = new List<Guid>();
        }

        /// <summary>
        /// Associate a preassembled vehicle with this vehicle order, if this vehicle order will be
        /// fulfilled by delivering a preassembled vehicle.
        /// </summary>
        /// <param name="preassembledVehicleId">Preassambled vehicle that matches all the critera in this vehicle order</param>
        public void AssociatePreassembledVehicle(Guid preassembledVehicleId)
        {
            PreassembledVehicleId = preassembledVehicleId;
        }

        public void AwaitPayment(Invoice invoice)
        {
            Status = VehicleOrderStatus.AwaitingPayment;
            Invoice = invoice;
        }

        public void Accept()
        {
            if ((Status != VehicleOrderStatus.AwaitingPayment && Status != VehicleOrderStatus.AllPartsReady) || Invoice is null)
            {
                throw new InvalidOperationException("Tried to accept a vehicle order that was not awaiting payment.");
            }

            if (PartsAwaitingManufacture.Any())
                Status = VehicleOrderStatus.WaitingForParts;
            else
                Status = VehicleOrderStatus.Accepted;

            Invoice.Pay();
        }

        public void AddPartAwaitingManufacture(Guid partId)
        {
            PartsAwaitingManufacture.Add(partId);
        }

        public void StopWaitingForPart(Guid partId)
        {
            PartsAwaitingManufacture.Remove(partId);

            if (!PartsAwaitingManufacture.Any())
                Status = VehicleOrderStatus.AllPartsReady;
        }

        public void Fullfil()
        {
            Status = VehicleOrderStatus.Fulfilled;
        }

        public void Cancel()
        {
            Status = VehicleOrderStatus.Cancelled;
        }
    }
}
