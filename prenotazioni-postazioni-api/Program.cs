using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net.AspNetCore;
using prenotazioni_postazioni_api.Controllers;
using prenotazioni_postazioni_api.Repositories;
using prenotazioni_postazioni_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add logging Log4Net
builder.Logging.AddLog4Net();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FestaController, FestaController>();
builder.Services.AddSingleton<FestaService, FestaService>();
builder.Services.AddSingleton<FestaRepository, FestaRepository>();

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
