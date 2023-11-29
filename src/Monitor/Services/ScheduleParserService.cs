using Parser.Abstractions;

namespace Parser.Services
{
    public class ScheduleParserService : BackgroundService
    {
        const int updateMins = 60;

        const int startDelay = 5000;

        private readonly ILogger _logger;

        public IServiceProvider Services { get; }

        public ScheduleParserService(ILogger<ScheduleParserService> logger,
            IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(startDelay);
            _logger.LogInformation("Schedule Parser Service running..");
            await ParseScheduleAsync(stoppingToken);

            using PeriodicTimer timer = new(TimeSpan.FromMinutes(updateMins));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await ParseScheduleAsync(stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Schedule Parser Service is stopping.");
            }
        }

        private async Task ParseScheduleAsync(CancellationToken stoppingToken)
        {
            try
            {
                using (var scope = Services.CreateScope())
                {
                    var scheduleRepo = scope.ServiceProvider.GetRequiredService<IScheduleRepository>();
                    await scheduleRepo.UpdateScheduleAsync(stoppingToken);
                }
            } catch (Exception ex)
            {
                _logger.LogError($"Schedule parsing ended with error: {ex.Message}, {ex.StackTrace}");
            }
        }
    }
}
