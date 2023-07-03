using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net.AspNetCore;
using prenotazioni_postazioni_api.Controllers;
using prenotazioni_postazioni_api.Repositories;
using prenotazioni_postazioni_api.Repositories.Database;
using prenotazioni_postazioni_api.Services;



var builder = WebApplication.CreateBuilder(args);

// Add logging Log4Net
builder.Logging.AddLog4Net("log4net.config");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FestaController, FestaController>();
builder.Services.AddSingleton<HolidayService, HolidayService>();
builder.Services.AddSingleton<HolidayRepository, HolidayRepository>();

builder.Services.AddSingleton<ImpostazioneController, ImpostazioneController>();
builder.Services.AddSingleton<SettingsRepository, SettingsRepository>();
builder.Services.AddSingleton<SettingsService, SettingsService>();

builder.Services.AddSingleton<PrenotazioneController, PrenotazioneController>();
builder.Services.AddSingleton<BookingService, BookingService>();
builder.Services.AddSingleton<BookingRepository, BookingRepository>();

builder.Services.AddSingleton<RuoloController, RuoloController>();
builder.Services.AddSingleton<RoleService, RoleService>();
builder.Services.AddSingleton<RoleRepository, RoleRepository>();

builder.Services.AddSingleton<StanzaController, StanzaController>();
builder.Services.AddSingleton<RoomService, RoomService>();
builder.Services.AddSingleton<RoomRepository, RoomRepository>();

builder.Services.AddSingleton<UtenteController, UtenteController>();
builder.Services.AddSingleton<UserService, UserService>();
builder.Services.AddSingleton<UserRepository, UserRepository>();

builder.Services.AddSingleton<VotoController, VotoController>();
builder.Services.AddSingleton<VoteService, VoteService>();
builder.Services.AddSingleton<VoteRepository, VoteRepository>();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
DatabaseInfo.DefaultConnectionString = connectionString;

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
