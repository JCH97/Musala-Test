using System;
using System.Threading;
using System.Threading.Tasks;
using MusalaTest.Domain.Entities;

namespace MusalaTest.Domain.Interfaces
{
    public interface IUseCase<TDEntity, Dto>
        where TDEntity : class
        where Dto : class
    {
        Task<TDEntity> Execute(Dto dto, CancellationToken ct = default);
    }
}