using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net.AspNetCore;
using prenotazioni_postazioni_api.Controllers;
using prenotazioni_postazioni_api.Repositories;
using prenotazioni_postazioni_api.Services;

var builder = WebApplication.CreateBuilder(args);

//Logging
using var loggerFactory = LoggerFactory.Create(loggingBuilding => loggingBuilding.SetMinimumLevel(LogLevel.Trace).AddConsole());

ILogger<FestaController> logger = loggerFactory.CreateLogger<FestaController>();


// Add logging Log4Net
builder.Logging.AddLog4Net();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FestaRepository, FestaRepository>();
builder.Services.AddSingleton<FestaService, FestaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
