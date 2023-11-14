using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories;
using API.PastillApp.Repositories.Interface;
using API.PastillApp.Services.Interfaces;
using API.PastillApp.Services.Services;

namespace API.PastillApp
{
    public class PastillAppWorker : BackgroundService
    {
        private readonly ILogger<PastillAppWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public PastillAppWorker(ILogger<PastillAppWorker> logger,IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await WaitForNextMinute();
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                    var reminderLogsRepository = serviceProvider.GetRequiredService<IReminderLogsRepository>();
                    var reminderService = serviceProvider.GetRequiredService<IReminderService>();

                    await ProcessSendNotification(reminderLogsRepository, reminderService);
                    await ProcessPostergerNotification(reminderLogsRepository, reminderService);
                    await ProcessNotificationEmergency(reminderLogsRepository, reminderService);
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task WaitForNextMinute()
        {
            var now = DateTime.Now;
            var nextMinute = now.AddMinutes(1).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);

            var delay = nextMinute - now;

            await Task.Delay(delay);
        }
        private async Task ProcessSendNotification(IReminderLogsRepository reminderLogsRepository, IReminderService reminderService)
        {
            List<ReminderLog> reminderLogs = reminderLogsRepository.GetReminderLogsNoTaken().Result;

            foreach (var reminderLog in reminderLogs)
            {
                var now = DateTime.Now;
                if (reminderLog.DateTime.Date == now.Date &&
                    reminderLog.DateTime.Hour == now.Hour &&
                    reminderLog.DateTime.Minute == now.Minute)
                {
                    await reminderService.SendAlarmNotification(reminderLog);
                }
            }
        }

        private async Task ProcessPostergerNotification(IReminderLogsRepository reminderLogsRepository, IReminderService reminderService)
        {
            List<ReminderLog> reminderLogs = reminderLogsRepository.GetReminderLogsNoTakenNotificated().Result;

            foreach (var reminderLog in reminderLogs)
            {
                DateTime reminderDate = reminderLog.DateTime;
                DateTime MinutesLater = reminderDate.AddMinutes(10);

                if (DateTime.Now >= MinutesLater)
                {
                    await reminderService.SendAlarmNotification(reminderLog);
                }
            }
        }

        private async Task ProcessNotificationEmergency(IReminderLogsRepository reminderLogsRepository, IReminderService reminderService)
        {
            List<ReminderLog> reminderLogs = reminderLogsRepository.GetReminderLogsToEmergency().Result;

            foreach (var reminderLog in reminderLogs)
            {
                DateTime reminderDate = reminderLog.DateTime;
                DateTime MinutesLater = reminderDate.AddMinutes(30);

                if (DateTime.Now >= MinutesLater)
                {
                    await reminderService.SendEmergencyNotification(reminderLog);
                }
            }
        }

    }
}
