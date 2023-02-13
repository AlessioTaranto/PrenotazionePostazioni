using Newtonsoft.Json;
using prenotazione_postazioni_libs.Models;
using System.Text;

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

        public async Task<HttpResponseMessage> getFesta(int year, int month, int day)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Festa");

            var httpResponseMessage =
                await httpClient.GetAsync($"https://localhost:7126/api/festivita/getByDate?year={year}&month={month}&day={day}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> AddFesta(DateTime date, string description)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Festa");

            string json = "{\"date\": \""+date.ToString("yyyy-MM-ddTHH:mm:ss") +"\", \"desc\": \""+description+"\"}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponseMessage =
                await httpClient.PostAsync($"https://localhost:7126/api/festivita/addFesta", content);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> RemoveFesta(int year, int month, int day)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Festa");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/festivita/removeFesta?year={year}&month={month}&day={day}");

            return httpResponseMessage;
        }

    }

}
