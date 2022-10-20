using Drones.Application.Services;
using Drones.Domain.DTOs;
using Drones.Domain.Repositories;
using Drones.Domain.Services;
using Drones.Infrastructure.Database;
using Drones.Infrastructure.HostedServices;
using Drones.Infrastructure.Repositories;
using Drones.Infrastructure.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using log4net;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssembly(typeof(DroneValidator).Assembly);
builder.Services.AddTransient<IValidator<DroneDto>, DroneValidator>();

builder.Services.AddValidatorsFromAssembly(typeof(MedicationValidator).Assembly);
builder.Services.AddTransient<IValidator<MedicationDto>, MedicationValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiContext>(x => x.UseInMemoryDatabase("DroneDB"));

builder.Services.SetupUnitOfWork();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load));

builder.Services.AddScoped<IDroneService, DroneService>();
builder.Services.AddScoped<IMedicationService, MedicationService>();
builder.Services.AddScoped<IDroneMedicationService, DroneMedicationService>();

builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddHostedService<LogBatteryLevelService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApiContext>();
    DataSeeding.Seed(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
