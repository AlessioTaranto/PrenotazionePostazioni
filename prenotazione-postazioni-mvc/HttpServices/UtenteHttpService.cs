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
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Utente");

            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/utenti/getAllUtenti");

            return httpResponseMessage;
        }

        public async Task<object> OnGetUtenteById()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Utente");

            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/impostazioni/getUtenteById");

            return httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK ? httpResponseMessage.Content : httpResponseMessage.StatusCode;
        }

    }
}
