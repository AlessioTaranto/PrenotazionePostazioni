using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Net.Http.Headers;
using prenotazione_postazioni_mvc.HttpServices;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

services.AddAuthentication(options => {
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
}).AddCookie(options => {
    options.LoginPath = "/Home/Login";
}).AddGoogle(GoogleDefaults.AuthenticationScheme, options => {
    options.ClientId = Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
    options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
});

// Dynamically set the base address for each HttpClient
var apiBaseUrl = Configuration.GetValue<string>("ApiPath"); // Fetch the API base URL from appsettings.json

// Define a helper action to configure HttpClient instances
Action<HttpClient> configureHttpClient = httpClient =>
{
    httpClient.BaseAddress = new Uri(apiBaseUrl); // Set the base address
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
};

// Use the helper action for each AddHttpClient call
builder.Services.AddHttpClient("PrenotazionePostazioni-Stanze", configureHttpClient);
builder.Services.AddHttpClient("PrenotazionePostazioni-Impostazioni", configureHttpClient);
builder.Services.AddHttpClient("PrenotazionePostazioni-Utente", configureHttpClient);
builder.Services.AddHttpClient("PrenotazionePostazioni-Voti", configureHttpClient);
builder.Services.AddHttpClient("PrenotazionePostazioni-Festa", configureHttpClient);
builder.Services.AddHttpClient("PrenotazionePostazioni-Postazioni", configureHttpClient);
builder.Services.AddHttpClient("PrenotazionePostazioni-Ruolo", configureHttpClient);
builder.Services.AddHttpClient("PrenotazionePostazioni-Capienza", configureHttpClient);
builder.Services.AddHttpClient("PrenotazionePostazioni-Menu", configureHttpClient);
builder.Services.AddHttpClient("PrenotazionePostazioni-MenuChoices", configureHttpClient);

// Registration of singleton services
builder.Services.AddSingleton<SettingsHttpService>();
builder.Services.AddSingleton<UserHttpService>();
builder.Services.AddSingleton<VoteHttpService>();
builder.Services.AddSingleton<CapacityHttpService>();
builder.Services.AddSingleton<HolidayHttpService>();
builder.Services.AddSingleton<BookingHttpSerivice>();
builder.Services.AddSingleton<RoleHttpService>();
builder.Services.AddSingleton<RoomHttpService>();
builder.Services.AddSingleton<MenuHttpService>();
builder.Services.AddSingleton<MenuChoicesHttpService>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins(apiBaseUrl, $"{apiBaseUrl}api/festivita/getAll");
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints => {
    endpoints.MapDefaultControllerRoute();
});

app.Use(async (context, next) =>
{
    await next.Invoke();
});

app.Run();
