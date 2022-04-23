using System.Collections.Generic;
using System.ComponentModel;
using MusalaTest.Application.Dtos.PeripheralDevices;
using MusalaTest.Domain.Enums;

namespace MusalaTest.Application.Dtos.Gateways
{
    public class CreateGatewayDto
    {
        public string Name { get; set; }

        public string Ip { get; set; }

        public List<CreatePeripheralDevicesDto> PeripheralDevices { get; set; } =
            new List<CreatePeripheralDevicesDto>();
    }
}