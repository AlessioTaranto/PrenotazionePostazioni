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

builder.Services.AddSingleton<HolidayController, HolidayController>();
builder.Services.AddSingleton<HolidayService, HolidayService>();
builder.Services.AddSingleton<HolidayRepository, HolidayRepository>();

builder.Services.AddSingleton<SettingsController, SettingsController>();
builder.Services.AddSingleton<SettingsRepository, SettingsRepository>();
builder.Services.AddSingleton<SettingsService, SettingsService>();

builder.Services.AddSingleton<BookingController, BookingController>();
builder.Services.AddSingleton<BookingService, BookingService>();
builder.Services.AddSingleton<BookingRepository, BookingRepository>();

builder.Services.AddSingleton<RoleController, RoleController>();
builder.Services.AddSingleton<RoleService, RoleService>();
builder.Services.AddSingleton<RoleRepository, RoleRepository>();

builder.Services.AddSingleton<RoomController, RoomController>();
builder.Services.AddSingleton<RoomService, RoomService>();
builder.Services.AddSingleton<RoomRepository, RoomRepository>();

builder.Services.AddSingleton<UserController, UserController>();
builder.Services.AddSingleton<UserService, UserService>();
builder.Services.AddSingleton<UserRepository, UserRepository>();

builder.Services.AddSingleton<VoteController, VoteController>();
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
