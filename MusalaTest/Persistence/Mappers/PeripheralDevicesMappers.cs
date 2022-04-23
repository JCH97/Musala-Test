using AutoMapper;
using MusalaTest.Application.Dtos.PeripheralDevices;
using MusalaTest.Domain.Entities;
using MusalaTest.Persistence.Entities;

namespace MusalaTest.Persistence.Mappers
{
    public class PeripheralDevicesMappers : Profile
    {
        public PeripheralDevicesMappers()
        {
            CreateMap<PeripheralDevice, PeripheralDevicePersistence>();

            CreateMap<PeripheralDevice, PeripheralDevicesDto>();

            CreateMap<PeripheralDevicePersistence, PeripheralDevice>();

            CreateMap<PeripheralDevicePersistence, PeripheralDevicesDto>();
        }
    }
}