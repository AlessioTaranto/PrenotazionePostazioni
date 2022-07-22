using Microsoft.Net.Http.Headers;
using prenotazione_postazioni_mvc.HttpServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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



builder.Services.AddSingleton<ImpostazioniHttpService>();
builder.Services.AddSingleton<UtenteHttpService>();

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
