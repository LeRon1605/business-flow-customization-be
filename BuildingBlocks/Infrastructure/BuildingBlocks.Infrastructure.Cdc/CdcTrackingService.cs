using System.Numerics;
using BuildingBlocks.EventBus.Abstracts;
using BuildingBlocks.EventBus.Enums;
using BuildingBlocks.Infrastructure.Cdc.Attributes;
using BuildingBlocks.Infrastructure.Cdc.Extensions;
using BuildingBlocks.Infrastructure.Cdc.Models;
using BuildingBlocks.Infrastructure.Cdc.Settings;
using BuildingBlocks.Infrastructure.Cdc.States;
using BuildingBlocks.Shared.Extensions;
using BuildingBlocks.Shared.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MsSqlCdc;

namespace BuildingBlocks.Infrastructure.Cdc;

public class CdcTrackingService : BackgroundService
{
    private readonly string _connectionString;
    private readonly CdcTrackingSetting _setting;
    private readonly ILogger<CdcTrackingService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ICdcStateService _cdcStateService;
    private CaptureTableStateModel? _state;
    private BigInteger _lowLsn = BigInteger.Zero;

    public CdcTrackingService(IConfiguration configuration
        , CdcTrackingSetting setting
        , ILogger<CdcTrackingService> logger
        , IServiceProvider serviceProvider
        , IHostApplicationLifetime hostApplicationLifetime
        , ICdcStateService cdcStateService)
    {
        _connectionString = configuration.GetRequiredValue<string>("ConnectionStrings:Default");
        _setting = setting;
        _logger = logger;
        _serviceProvider = serviceProvider;
        _hostApplicationLifetime = hostApplicationLifetime;
        _cdcStateService = cdcStateService;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
        
        var tables = CaptureTableModelExtensions.GetCdcTables();
        _state = await _cdcStateService.GetLastProcessedLsnAsync(AssemblyHelper.GetServiceName(), cancellationToken);
        
        _lowLsn = _state?.LastProcessedLsn ?? await GetStartLsn();

        while (true)
        {
            await using var connection = new SqlConnection(_connectionString);

            try
            {
                await connection.OpenAsync(cancellationToken);

                var highLsn = await MsSqlCdc.Cdc.GetMaxLsnAsync(connection);
                if (_lowLsn <= highLsn)
                {
                    _logger.LogInformation("Polling from {LowLsn} to {HighLsn}", _lowLsn, highLsn);

                    var changes = new Dictionary<Type, List<NetChangeRow>>();
                    foreach (var table in tables)
                    {
                        var tableChanges = await MsSqlCdc.Cdc.GetNetChangesAsync(connection
                            , table.GetTableCaptureName()
                            , _lowLsn
                            , highLsn);

                        changes.Add(table, tableChanges.ToList());
                    }

                    await ProcessChangeAsync(changes);

                    _lowLsn = await MsSqlCdc.Cdc.GetNextLsnAsync(connection, highLsn);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while polling CDC changes: {Message}", ex.Message);
            }
            finally
            {
                await Task.Delay(_setting.IntervalPollingMs, cancellationToken);
            }
        }
    }
    
    private void OnStopping()
    {
        _logger.LogInformation("CDC tracking service is stopping at {LowLsn}.", _lowLsn);
        
        if (_state == null)
            _cdcStateService.SaveLastProcessedLsnAsync(AssemblyHelper.GetServiceName(), _lowLsn, CancellationToken.None).Wait();
        else
            _cdcStateService.UpdateLastProcessedLsnAsync(_state.Id, _lowLsn, CancellationToken.None).Wait();
    }
    
    private async Task<BigInteger> GetStartLsn()
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var currentMaxLsn = await MsSqlCdc.Cdc.GetMaxLsnAsync(connection);
        return await MsSqlCdc.Cdc.GetNextLsnAsync(connection, currentMaxLsn);
    }

    private async Task ProcessChangeAsync(Dictionary<Type, List<NetChangeRow>> changes)
    {
        if (!changes.Any())
            return;
        
        await using var scope = _serviceProvider.CreateAsyncScope();
        var eventPublisher = scope.ServiceProvider.GetRequiredService<IEventPublisher>();

        foreach (var table in changes)
        {
            foreach (var change in table.Value)
            {
                var obj = GetModel(table.Key, change);
                var model = Convert.ChangeType(obj, table.Key);

                var integrationEvent = (model as ICaptureTableModel)?.GetIntegrationEvent(GetAction(change));
                if (integrationEvent == null)
                    return;

                await eventPublisher.Publish(integrationEvent);
            }
        }
    }

    private object GetModel(Type type, NetChangeRow change)
    {
        var obj = Activator.CreateInstance(type);
        if (obj == null)
            throw new InvalidOperationException($"Cannot create instance of {type.Name}");
        
        var objType = obj.GetType();

        foreach (var item in change.Fields)
        {
            objType
                .GetProperty(item.Key)
                ?.SetValue(obj, item.Value, null);
        }

        return obj;
    }
    
    private EntityAction GetAction(NetChangeRow change)
    {
        return change.Operation switch
        {
            NetChangeOperation.Insert => EntityAction.Insert,
            NetChangeOperation.Update => EntityAction.Update,
            NetChangeOperation.Delete => EntityAction.Delete,
        };
    }
}