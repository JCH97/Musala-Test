using System;
using MusalaTest.Domain.Enums;

namespace MusalaTest.Application.Dtos.PeripheralDevices
{
    public class PeripheralDevicesDto
    {
        public Guid Id { get; set; }

        public string Vendor { get; set; }

        public PeripheralStatus Status { get; set; }

        public Guid GatewayId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}