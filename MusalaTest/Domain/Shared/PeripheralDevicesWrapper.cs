using MusalaTest.Domain.Entities;
using MusalaTest.Domain.Interfaces;

namespace MusalaTest.Domain.Shared
{
    public class PeripheralDevicesWrapper : ISimpleWrapper<PeripheralDevice>
    {
        public bool IsSuccess { get; set; }

        public PeripheralDevice Value { get; set; }

        public string ErrorMessage { get; set; }
    }
}