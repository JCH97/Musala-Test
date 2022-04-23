using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusalaTest.Application.Dtos.Gateways;
using MusalaTest.Domain.Entities;
using MusalaTest.Domain.Interfaces;
using MusalaTest.Persistence;

namespace MusalaTest.Application.UseCases.Gateway
{
    public class FetchGatewaysDetails : IUseCase<GatewayDetailsDto, FetchGatewaysDetailsDto>
    {
        private readonly IGatewayRepository _repository;
        private readonly IMapper _mapper;

        public FetchGatewaysDetails(IMapper mapper, IGatewayRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GatewayDetailsDto> Execute(FetchGatewaysDetailsDto dto, CancellationToken ct = default)
        {
            var g = await _repository.GetDetails(dto.Id, ct);

            if (g == null)
                throw new Exception("Gateway not found");

            return _mapper.Map<GatewayDetailsDto>(g);
        }
    }
}