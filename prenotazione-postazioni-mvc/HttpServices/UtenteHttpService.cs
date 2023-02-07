using Microsoft.AspNetCore.Mvc;

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

        public async Task<HttpResponseMessage> OnGetUtenteById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Utente");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/impostazioni/getUtenteById?id={id}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnGetUtenteByEmail(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Utente");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/impostazioni/getUtenteByEmail?email={id}");

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
        }

    }
}
