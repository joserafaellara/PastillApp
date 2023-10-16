using API.PastillApp.Repositories;
using API.PastillApp.Repositories.Interface;
using API.PastillApp.Services.Interfaces;
using API.PastillApp.Services.Services;
using WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<IReminderLogsRepository, ReminderLogRepository>();
        services.AddTransient<IReminderService, ReminderService>();
    })
    .Build();

await host.RunAsync();
