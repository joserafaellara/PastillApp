using API.PastillApp.Repositories;
using API.PastillApp.Repositories.Interface;
using API.PastillApp.Services.Interfaces;
using API.PastillApp.Services.Mapper;
using API.PastillApp.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PastillAppContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PastillAppCS"));
    //options.UseInMemoryDatabase("PastillAppDB");
}
    );

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAlertLogRepository, AlertLogRepository>();
builder.Services.AddScoped<IReminderRepository, ReminderRepository>();

#region SERVICES

builder.Services
    .AddTransient<IUserService, UserService>();
    

#endregion

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
