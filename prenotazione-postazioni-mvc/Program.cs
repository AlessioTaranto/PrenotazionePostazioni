using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Net.Http.Headers;
using prenotazione_postazioni_mvc.HttpServices;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
}).AddCookie().AddGoogle(GoogleDefaults.AuthenticationScheme, options => 
{
    options.ClientId = "989509766036-m7kb2is391d96vcs669p9t808ij9kjlp.apps.googleusercontent.com";
    options.ClientSecret = "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";
    options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
});

builder.Services.AddHttpClient("PrenotazionePostazioni-Stanze", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7126/api/stanze/");

    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddHttpClient("PrenotazionePostazioni-Impostazioni", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7126/api/impostazioni/");

    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddHttpClient("PrenotazionePostazioni-Utente", HttpClient =>
{
    HttpClient.BaseAddress = new Uri("https://localhost:7126/api/utenti/");

    HttpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddHttpClient("PrenotazionePostazioni-Voti", HttpClient =>
{
    HttpClient.BaseAddress = new Uri("https://localhost:7126/api/voti/");

    HttpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddHttpClient("PrenotazionePostazioni-Festa", HttpClient =>
{
    HttpClient.BaseAddress = new Uri("https://localhost:7126/api/festivita/");

    HttpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddHttpClient("PrenotazionePostazioni-Postazioni", HttpClient =>
{
    HttpClient.BaseAddress = new Uri("https://localhost:7126/api/postazioni/");

    HttpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddHttpClient("PrenotazionePostazioni-Ruolo", HttpClient =>
{
    HttpClient.BaseAddress = new Uri("https://localhost:7126/api/ruoli/");

    HttpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddHttpClient("PrenotazionePostazioni-Capienza", HttpClient =>
{
    HttpClient.BaseAddress = new Uri("https://localhost:7126/api/stanze/");

    HttpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});


builder.Services.AddHttpClient("PrenotazionePostazioni-Menu", HttpClient =>
{
    HttpClient.BaseAddress = new Uri("https://localhost:7126/api/menu/");

    HttpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});


builder.Services.AddHttpClient("PrenotazionePostazioni-MenuChoices", HttpClient =>
{
    HttpClient.BaseAddress = new Uri("https://localhost:7126/api/menuChoices/");

    HttpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

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
            policy.WithOrigins("https://localhost:7126/",
                                "https://localhost:7126/api/festivita/getAll");
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints => {
    endpoints.MapDefaultControllerRoute();
});

app.Use(async (context, next) =>
{
    // Do work that can write to the Response.
    await next.Invoke();
    // Do logging or other work that doesn't write to the Response.
});

app.Run(async context =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
});

app.Run();
