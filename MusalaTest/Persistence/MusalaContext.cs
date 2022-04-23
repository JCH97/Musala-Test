using System.Threading;
using Microsoft.EntityFrameworkCore;
using MusalaTest.Persistence.Entities;

namespace MusalaTest.Persistence
{
    public class MusalaContext : DbContext
    {
        public MusalaContext(DbContextOptions<MusalaContext> options) : base(options)
        {
        }

        public DbSet<GatewaysPersistence> Gateway { get; set; }

        public DbSet<PeripheralDevicePersistence> PeripheralDevice { get; set; }
    }
}