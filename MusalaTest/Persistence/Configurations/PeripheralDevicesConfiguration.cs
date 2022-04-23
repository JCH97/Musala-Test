using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusalaTest.Persistence.Entities;

namespace MusalaTest.Persistence.Configurations
{
    public class PeripheralDevicesConfiguration : IEntityTypeConfiguration<PeripheralDevicePersistence>
    {
        public void Configure(EntityTypeBuilder<PeripheralDevicePersistence> builder)
        {
            builder.HasOne(x => x.Gateways)
                .WithMany(o => o.PeripheralDevices)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}