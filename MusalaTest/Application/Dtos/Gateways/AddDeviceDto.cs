using System;
using MusalaTest.Application.Dtos.PeripheralDevices;

namespace MusalaTest.Application.Dtos.Gateways
{
    public class AddDeviceDto : CreatePeripheralDevicesDto
    {
        public Guid GatewayId { get; set; }
    }
}