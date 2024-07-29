using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Application.BackgroundServices
{
    /// <summary>
    /// The periodic award service class.
    /// </summary>
    public class PeriodicAwardService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger logger;
        private Timer? timer;

        /// <summary>Initializes a new instance of the <see cref="PeriodicAwardService" /> class.</summary>
        /// <param name="serviceScopeFactory">The service scope factory.</param>
        /// <param name="logger">The logger.</param>
        public PeriodicAwardService(IServiceScopeFactory serviceScopeFactory, ILogger logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            timer?.Dispose();
        }

        /// <summary>Triggered when the application host is ready to start the service.</summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task">Task</see> that represents the asynchronous Start operation.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.Information("Starting distributing periodic awards.");
            timer = new Timer(DistributeAwards, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        /// <summary>Triggered when the application host is performing a graceful shutdown.</summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task">Task</see> that represents the asynchronous Stop operation.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.Information("Finished distributing periodic awards.");
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Distributes awards.
        /// </summary>
        /// <param name="state"></param>
        private async void DistributeAwards(object? state)
        {
            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var awardsService = scope.ServiceProvider.GetRequiredService<IAwardsService>();
                    await awardsService.DistributeAwardsAsync();
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error occurred while distributing awards.");
            }
        }
    }
}
