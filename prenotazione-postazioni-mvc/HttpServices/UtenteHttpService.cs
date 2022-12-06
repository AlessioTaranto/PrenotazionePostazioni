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
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Utente");

            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/utenti/getAllUtenti");

            return httpResponseMessage;
        }

<<<<<<< HEAD
        public async Task<HttpResponseMessage> OnGetUtenteById(int id)
=======
        public async Task<Utente?> OnGetUtenteById(int idUtente)
>>>>>>> origin/feature/prenotazioni
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Utente");

<<<<<<< HEAD
            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/impostazioni/getUtenteById?id={id}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnGetUtentiPrenotatiByDay(DateTime date)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Utente");

            var year = date.Year;
            var month = date.Month;
            var day = date.Day;

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/utenti/getUtentiPrenotatiByDay?year={year}&month={month}&day={day}");

            return httpResponseMessage;
=======
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
>>>>>>> origin/feature/prenotazioni
        }

    }
}
