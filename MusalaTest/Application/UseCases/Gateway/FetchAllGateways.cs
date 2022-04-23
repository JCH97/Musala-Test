using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusalaTest.Application.Dtos.Gateways;
using MusalaTest.Domain.Entities;
using MusalaTest.Domain.Interfaces;

namespace MusalaTest.Application.UseCases.Gateway
{
    public class FetchAllGateways : IUseCase<List<GatewayDto>, string>
    {
        private readonly IBaseRepository<Gateways> _repository;
        private readonly IMapper _mapper;

        public FetchAllGateways(IBaseRepository<Gateways> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GatewayDto>> Execute(string dto, CancellationToken ct = default)
        {
            var list = await _repository
                .GetQuery()
                .ToListAsync(ct);

            return list.Select(_mapper.Map<GatewayDto>).ToList();
        }
    }
}