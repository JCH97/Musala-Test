using System;
using MusalaTest.Domain.Enums;

namespace MusalaTest.Persistence.Entities
{
    public class PeripheralDevicePersistence
    {
        public Guid Id { get; set; }

        public string Vendor { get; set; }

        public PeripheralStatus Status { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public Guid Gateway { get; set; }

        public virtual GatewaysPersistence Gateways { get; set; }
    }
}