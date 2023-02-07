using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Net.Http.Headers;
using prenotazione_postazioni_mvc.HttpServices;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
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
    HttpClient.BaseAddress = new Uri("https://localhost:7126/api/impostazioni/");

    HttpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddHttpClient("PrenotazionePostazioni-Voti", HttpClient =>
{
    HttpClient.BaseAddress = new Uri("https://localhost:7126/api/impostazioni/");

    HttpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});



builder.Services.AddSingleton<ImpostazioniHttpService>();
builder.Services.AddSingleton<UtenteHttpService>();
builder.Services.AddSingleton<VotoHttpService>();
builder.Services.AddSingleton<CapienzaHttpService>();
builder.Services.AddSingleton<FestaHttpService>();

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

app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints => {
    endpoints.MapDefaultControllerRoute();
});

app.Run();
