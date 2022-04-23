using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MusalaTest.Application.Dtos.Gateways;
using MusalaTest.Domain.Entities;
using MusalaTest.Domain.Interfaces;
using MusalaTest.Persistence.Entities;

namespace MusalaTest.Application.UseCases.Gateway
{
    public class AddDeviceUseCase : IUseCase<string, AddDeviceDto>
    {
        private readonly IGatewayRepository _repository;
        private readonly IMapper _mapper;

        public AddDeviceUseCase(IGatewayRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> Execute(AddDeviceDto dto, CancellationToken ct = default)
        {
            var device = PeripheralDevice.Create(dto.Vendor, dto.Status);

            if (!device.IsSuccess)
                throw new Exception(device.ErrorMessage);

            await _repository.AddDevice(dto.GatewayId, _mapper.Map<PeripheralDevicePersistence>(device.Value), ct);

            return null;
        }
    }
}