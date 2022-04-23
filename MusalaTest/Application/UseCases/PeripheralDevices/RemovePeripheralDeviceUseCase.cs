using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MusalaTest.Application.Dtos.Gateways;
using MusalaTest.Application.Dtos.PeripheralDevices;
using MusalaTest.Domain.Entities;
using MusalaTest.Domain.Interfaces;

namespace MusalaTest.Application.UseCases.PeripheralDevices
{
    public class RemovePeripheralDeviceUseCase : IUseCase<string, RemovePeripheralDeviceDto>
    {
        private readonly IBaseRepository<PeripheralDevice> _repository;
        private readonly IMapper _mapper;

        public RemovePeripheralDeviceUseCase(IMapper mapper, IBaseRepository<PeripheralDevice> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<string> Execute(RemovePeripheralDeviceDto dto, CancellationToken ct)
        {
            var gateway = await _repository.FindReadOnly(x => x.Id == dto.Id, ct);
            if (gateway == null)
                throw new Exception("Gateway not found");

            await _repository.DeleteOneAndSave(gateway, ct);

            return null;
        }
    }
}