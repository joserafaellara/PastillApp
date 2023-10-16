using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories.Interface;
using API.PastillApp.Services.Interfaces;
using System.Diagnostics.Metrics;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IReminderLogsRepository _reminderLogsRepository;
        private readonly IReminderService _remindeService;

        public Worker(ILogger<Worker> logger, IReminderLogsRepository reminderLogsRepository, IReminderService reminderService)
        {
            _logger = logger;
            _reminderLogsRepository = reminderLogsRepository;
            _remindeService = reminderService; 
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await ProcessSendNotification();

                await ProcessPostergerNotification();

                await ProcessNotificationEmergency();

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task ProcessSendNotification()
        {
            List<ReminderLog> reminderLogs = _reminderLogsRepository.GetReminderLogsNoTaken().Result;

            foreach (var reminderLog in reminderLogs)
            {
                if (reminderLog.DateTime == DateTime.Now.Date)
                {
                    await _remindeService.SendAlarmNotification(reminderLog);
                }
            } 
        }

        private async Task ProcessPostergerNotification()
        {
            List<ReminderLog> reminderLogs = _reminderLogsRepository.GetReminderLogsNoTakenNotificated().Result;

            foreach (var reminderLog in reminderLogs)
            {
                DateTime reminderDate = reminderLog.DateTime;
                DateTime MinutesLater = reminderDate.AddMinutes(10);

                if (DateTime.Now >= MinutesLater)
                {
                    await _remindeService.SendAlarmNotification(reminderLog);
                }
            }
        }

        private async Task ProcessNotificationEmergency()
        {
            List<ReminderLog> reminderLogs = _reminderLogsRepository.GetReminderLogsToEmergency().Result;

            foreach (var reminderLog in reminderLogs)
            {
                DateTime reminderDate = reminderLog.DateTime;
                DateTime MinutesLater = reminderDate.AddMinutes(30);

                if (DateTime.Now >= MinutesLater)
                {
                    await _remindeService.SendEmergencyNotification(reminderLog);
                }
            }
        }

    }
}