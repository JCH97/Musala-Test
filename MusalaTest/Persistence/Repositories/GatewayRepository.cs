using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusalaTest.Application.Dtos.Gateways;
using MusalaTest.Application.Dtos.PeripheralDevices;
using MusalaTest.Domain.Entities;
using MusalaTest.Domain.Interfaces;
using MusalaTest.Domain.Shared;
using MusalaTest.Persistence.Entities;

namespace MusalaTest.Persistence.Repositories
{
    public class GatewayRepository : BaseRepository<Gateways, GatewaysPersistence, MusalaContext>, IGatewayRepository
    {
        public GatewayRepository(MusalaContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<GatewayDetailsDto> GetDetails(Guid id, CancellationToken ct)
        {
            var g = await Context
                .Gateway
                .Include(x => x.PeripheralDevices)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            return g == null ? null : Mapper.Map<GatewayDetailsDto>(g);
        }

        public async Task AddDevice(Guid gatewayId, PeripheralDevicePersistence device, CancellationToken ct)
        {
            var g = await Context
                .Gateway
                .Include(x => x.PeripheralDevices)
                .FirstOrDefaultAsync(x => x.Id == gatewayId, ct);

            Context.PeripheralDevice.Add(device);

            var temp = g.PeripheralDevices?.ToList() ?? new List<PeripheralDevicePersistence>();
            temp.Add(device);
            g.PeripheralDevices = temp;

            await Context.SaveChangesAsync(ct);
        }
    }
}