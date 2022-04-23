using MusalaTest.Domain.Enums;

namespace MusalaTest.Application.Dtos.PeripheralDevices
{
    public class CreatePeripheralDevicesDto
    {
        public string Vendor { get; set; }

        public PeripheralStatus Status { get; set; }
    }
}