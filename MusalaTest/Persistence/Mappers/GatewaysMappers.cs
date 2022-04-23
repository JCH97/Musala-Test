using AutoMapper;
using MusalaTest.Application.Dtos.Gateways;
using MusalaTest.Domain.Entities;
using MusalaTest.Persistence.Entities;

namespace MusalaTest.Persistence.Mappers
{
    public class GatewaysMappers : Profile
    {
        public GatewaysMappers()
        {
            CreateMap<Gateways, GatewaysPersistence>();

            CreateMap<GatewaysPersistence, Gateways>();

            CreateMap<GatewaysPersistence, GatewayDto>();

            CreateMap<Gateways, GatewayDto>();

            CreateMap<GatewaysPersistence, GatewayDetailsDto>();

            CreateMap<Gateways, GatewayDetailsDto>();
        }
    }
}