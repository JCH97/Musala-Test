using AutoMapper;
using MusalaTest.Domain.Entities;
using MusalaTest.Domain.Shared;
using MusalaTest.Persistence.Entities;

namespace MusalaTest.Persistence.Repositories
{
    public class PeripheralRepository : BaseRepository<PeripheralDevice, PeripheralDevicePersistence, MusalaContext>
    {
        public PeripheralRepository(MusalaContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}