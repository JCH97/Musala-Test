using System.Collections.Generic;
using MusalaTest.Application.Dtos.PeripheralDevices;

namespace MusalaTest.Application.Dtos.Gateways
{
    public class GatewayDetailsDto : GatewayDto
    {
        public List<PeripheralDevicesDto> PeripheralDevices { get; set; }
    }
}