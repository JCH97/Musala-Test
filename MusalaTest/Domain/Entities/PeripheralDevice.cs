using System;
using MusalaTest.Domain.Enums;
using MusalaTest.Domain.Shared;

namespace MusalaTest.Domain.Entities
{
    public class PeripheralDevice : BaseEntity
    {
        public PeripheralDevice()
        {
        }

        private PeripheralDevice(Guid id, string vendor, PeripheralStatus status)
        {
            Id = id;
            Vendor = vendor;
            Status = status;

            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
        }


        public static PeripheralDevicesWrapper Create(string vendor, PeripheralStatus status)
        {
            var d = new PeripheralDevice(Guid.NewGuid(), vendor, status);

            return new PeripheralDevicesWrapper
            {
                Value = d,
                ErrorMessage = null,
                IsSuccess = true
            };
        }


        public void Update(string vendor, PeripheralStatus? status)
        {
            Vendor = vendor ?? Vendor;
            Status = status ?? Status;
        }

        public string Vendor { get; private set; }

        public PeripheralStatus Status { get; private set; }

        public Guid GatewayId { get; private set; }

        public virtual Gateways Gateways { get; private set; }
    }
}