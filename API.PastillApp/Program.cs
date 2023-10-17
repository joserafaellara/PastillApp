using API.PastillApp;
using API.PastillApp.Repositories;
using API.PastillApp.Repositories.Interface;
using API.PastillApp.Services.Interfaces;
using API.PastillApp.Services.Mapper;
using API.PastillApp.Services.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


     var firebase = FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromFile("Properties/Firebase.json")
    });

    builder.Services.AddSingleton(firebase);


builder.Services.AddDbContext<PastillAppContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PastillAppCS"));
    //options.UseInMemoryDatabase("PastillAppDB");
}
    );
builder.Services.AddHostedService<PastillAppWorker>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAlertLogRepository, AlertLogRepository>();
builder.Services.AddScoped<IReminderRepository, ReminderRepository>();
builder.Services.AddScoped<IDailyStatusRepository, DailyStatusRepository>();
builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IReminderLogsRepository, ReminderLogRepository>();


#region SERVICES

builder.Services
    .AddTransient<IUserService, UserService>()
    .AddTransient<IAlertLogService, AlertLogService>()
    .AddTransient<IReminderService, ReminderService>()
    .AddTransient<IMedicineService, MedicineService>()
    .AddTransient<IDailyStatusService, DailyStatusService>()
    .AddTransient<ITokenService, TokenService>();
#endregion


//builder.Services.AddScoped<PastillAppWorker>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PastillAppContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API.PastillApp");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

