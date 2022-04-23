using System;
using System.Threading;
using System.Threading.Tasks;
using MusalaTest.Application.Dtos.Gateways;
using MusalaTest.Application.Dtos.PeripheralDevices;
using MusalaTest.Domain.Entities;
using MusalaTest.Persistence.Entities;

namespace MusalaTest.Domain.Interfaces
{
    public interface IGatewayRepository : IBaseRepository<Gateways>
    {
        public Task<GatewayDetailsDto> GetDetails(Guid id, CancellationToken ct);

        public Task AddDevice(Guid gatewayId, PeripheralDevicePersistence device, CancellationToken ct);
    }
}