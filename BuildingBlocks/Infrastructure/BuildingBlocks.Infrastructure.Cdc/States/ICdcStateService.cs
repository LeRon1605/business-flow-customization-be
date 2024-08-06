using System.Numerics;
using BuildingBlocks.Infrastructure.Cdc.Models;
using BuildingBlocks.Kernel.Services;

namespace BuildingBlocks.Infrastructure.Cdc.States;

public interface ICdcStateService : IScopedService
{
    Task<CaptureTableStateModel?> GetLastProcessedLsnAsync(string name, CancellationToken cancellationToken);

    Task SaveLastProcessedLsnAsync(string name
        , BigInteger lastLsn
        , CancellationToken cancellationToken);
    
    Task UpdateLastProcessedLsnAsync(Guid id
        , BigInteger lastLsn
        , CancellationToken cancellationToken);
}