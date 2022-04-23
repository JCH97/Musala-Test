using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusalaTest.Application.Dtos.PeripheralDevices;
using MusalaTest.Domain.Interfaces;

namespace MusalaTest.Presentation.Controllers
{
    [ApiController]
    [Route("api/devices")]
    public class PeripheralController
    {
        private readonly IUseCase<string, RemovePeripheralDeviceDto> _removeUseCase;

        public PeripheralController(IUseCase<string, RemovePeripheralDeviceDto> removeUseCase)
        {
            _removeUseCase = removeUseCase;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> RemoveDevice(Guid id, CancellationToken ct)
        {
            return await _removeUseCase.Execute(new RemovePeripheralDeviceDto
            {
                Id = id
            }, ct);
        }
    }
}