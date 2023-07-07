using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class StanzeHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StanzeHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> OnGetAllStanze()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Stanze");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/room/getAll");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnGetStanzaById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Stanze");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/room/getById?id={id}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnGetStanzaByName(string name)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Stanze");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/room/getByName?stanzaName={name}");

            return httpResponseMessage;
        }
    }
}
