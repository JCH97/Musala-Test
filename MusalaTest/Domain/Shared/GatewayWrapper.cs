using MusalaTest.Application.Dtos.Gateways;
using MusalaTest.Domain.Entities;
using MusalaTest.Domain.Interfaces;

namespace MusalaTest.Domain.Shared
{
    public class GatewayWrapper : ISimpleWrapper<Gateways>
    {
        public bool IsSuccess { get; set; }

        public Gateways Value { get; set; }

        public string ErrorMessage { get; set; }
    }
}