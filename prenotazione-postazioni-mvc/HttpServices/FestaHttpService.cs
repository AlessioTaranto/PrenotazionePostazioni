using Newtonsoft.Json;
using prenotazione_postazioni_libs.Models;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class FestaHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FestaHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> getAllFeste()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Festa");

            var httpResponseMessage =
                await httpClient.GetAsync($"https://localhost:7126/api/festivita/getAll");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> AddFesta(DateTime date)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Festa");

            Festa festa = new Festa(date);
            string json = JsonConvert.SerializeObject(festa);
            var content = new StringContent(json);

            var httpResponseMessage =
                await httpClient.PostAsync($"https://localhost:7126/api/festivita/addFesta", content);

            return httpResponseMessage;
        }

    }

}
