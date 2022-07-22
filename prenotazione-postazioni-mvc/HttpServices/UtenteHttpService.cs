using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_libs.Models;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class UtenteHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UtenteHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> OnGetAllUtenti()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Utente");

            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/utenti/getAllUtenti");

            return httpResponseMessage;
        }

        public async Task<Utente?> OnGetUtenteById(int idUtente)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Utente");

            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/impostazioni/getUtenteById?idUtente=" + idUtente);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Utente? utente = await httpResponseMessage.Content.ReadFromJsonAsync<Utente>();
                return utente;
            }
            return null;
        }

        public async Task<List<Utente>?> OnGetUtentiPrenotatiByDay(DateTime date)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/getUtentiPrenotatiByDay?date={date.ToString("yyyy-MM-ddTHH:mm:ss")}");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                List<Utente>? utenti = await httpResponseMessage.Content.ReadFromJsonAsync<List<Utente>>();
                return utenti;
            }
            return null;
        }

    }
}
