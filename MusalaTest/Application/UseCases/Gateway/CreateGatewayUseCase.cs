using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MusalaTest.Application.Dtos.Gateways;
using MusalaTest.Application.Dtos.PeripheralDevices;
using MusalaTest.Domain.Entities;
using MusalaTest.Domain.Interfaces;

namespace MusalaTest.Application.UseCases.Gateway
{
    public class CreateGatewayUseCase : IUseCase<GatewayDto, CreateGatewayDto>
    {
        private readonly IBaseRepository<Gateways> _repository;
        private readonly IMapper _mapper;

        public CreateGatewayUseCase(IBaseRepository<Gateways> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GatewayDto> Execute(CreateGatewayDto dto, CancellationToken ct)
        {
            var devices = new List<PeripheralDevice>();

            foreach (var d in dto.PeripheralDevices)
            {
                var temp = PeripheralDevice.Create(d.Vendor, d.Status);
                if (!temp.IsSuccess)
                    throw new Exception(temp.ErrorMessage);

                devices.Add(temp.Value);
            }

            var domain = Gateways.Create(dto.Name, dto.Ip, devices);

            if (!domain.IsSuccess)
                throw new Exception(domain.ErrorMessage);


            var p = await _repository
                .CreateOne(domain.Value, ct);

            return _mapper.Map<GatewayDto>(p);
        }
    }
}