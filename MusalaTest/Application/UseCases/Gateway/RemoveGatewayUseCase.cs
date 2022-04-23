using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MusalaTest.Application.Dtos.Gateways;
using MusalaTest.Domain.Entities;
using MusalaTest.Domain.Interfaces;

namespace MusalaTest.Application.UseCases.Gateway
{
    public class RemoveGatewayUseCase : IUseCase<string, RemoveGatewayDto>
    {
        private readonly IBaseRepository<Gateways> _repository;
        private readonly IMapper _mapper;

        public RemoveGatewayUseCase(IMapper mapper, IBaseRepository<Gateways> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<string> Execute(RemoveGatewayDto dto, CancellationToken ct)
        {
            var gateway = await _repository.FindReadOnly(x => x.Id == dto.Id, ct);
            if (gateway == null)
                throw new Exception("Gateway not found");

            await _repository.DeleteOneAndSave(gateway, ct);

            return null;
        }
    }
}