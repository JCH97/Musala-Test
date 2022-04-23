using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusalaTest.Application.Dtos.Gateways;
using MusalaTest.Application.Dtos.PeripheralDevices;
using MusalaTest.Domain.Entities;
using MusalaTest.Domain.Interfaces;
using MusalaTest.Persistence;
using MusalaTest.Persistence.Entities;

namespace MusalaTest.Presentation.Controllers
{
    [ApiController]
    [Route("api/gateway")]
    public class MusalaController : ControllerBase
    {
        private readonly IUseCase<GatewayDto, CreateGatewayDto> _createGatewayUseCase;
        private readonly IUseCase<List<GatewayDto>, string> _findAllUseCase;
        private readonly IUseCase<GatewayDetailsDto, FetchGatewaysDetailsDto> _fetchDetails;
        private readonly IUseCase<string, AddDeviceDto> _addDeviceUseCase;

        public MusalaController(
            IUseCase<GatewayDto, CreateGatewayDto> createGatewayUseCase,
            IUseCase<List<GatewayDto>, string> findAllUseCase,
            IUseCase<GatewayDetailsDto, FetchGatewaysDetailsDto> fetchDetails,
            IUseCase<string, AddDeviceDto> addDeviceUseCase)
        {
            _createGatewayUseCase = createGatewayUseCase;
            _findAllUseCase = findAllUseCase;
            _fetchDetails = fetchDetails;
            _addDeviceUseCase = addDeviceUseCase;
        }

        [HttpPost]
        public async Task<ActionResult<GatewayDto>> Post(CreateGatewayDto dto, CancellationToken ct)
        {
            return await _createGatewayUseCase.Execute(dto, ct);
        }

        [HttpPost("add-device")]
        public async Task<ActionResult<string>> AddDevice(AddDeviceDto dto, CancellationToken ct)
        {
            return await _addDeviceUseCase.Execute(dto, ct);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<GatewayDto>>> Get(CancellationToken ct)
        {
            return await _findAllUseCase.Execute(null, ct);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GatewayDetailsDto>> Details(Guid id, CancellationToken ct)
        {
            return await _fetchDetails.Execute(new FetchGatewaysDetailsDto {Id = id}, ct);
        }
    }
}