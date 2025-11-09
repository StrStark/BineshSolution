using BineshSoloution.Dtos.Panel;
using BineshSoloution.Interfaces.Sales;

namespace BineshSoloution.Workers;

public partial class PanelComputationWorker : BackgroundService
{
    [AutoInject] private readonly PanelDataCache _cache = default!;
    [AutoInject] private readonly IServiceScopeFactory _scopeFactory = default!;
    [AutoInject] private readonly ILogger<PanelComputationWorker> _logger = default!;
    [AutoInject] private readonly AppSettings _appSettings = default!;
    /*
     Inject Needed Services here ...
     */

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();

            try
            {
                // run your heavy logic here
               

                _logger.LogInformation($"Dashboard cache updated at {_cache.LastUpdated}",);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating dashboard cache");
            }

            await Task.Delay(_appSettings.PanelWorker.Interval, cancellationToken);
        }
    }
}
