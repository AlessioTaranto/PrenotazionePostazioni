using Newtonsoft.Json;
using prenotazione_postazioni_libs.Models;
using System.Text;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class HolidayHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HolidayHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Holiday");

            var httpResponseMessage =
                await httpClient.GetAsync($"https://localhost:7126/api/holiday/getAll");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByDate(int year, int month, int day)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Holiday");

            var httpResponseMessage =
                await httpClient.GetAsync($"https://localhost:7126/api/holiday/getByDate?year={year}&month={month}&day={day}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> Add(DateTime date, string description)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Holiday");

            string json = "{\"date\": \""+date.ToString("yyyy-MM-ddTHH:mm:ss") +"\", \"desc\": \""+description+"\"}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponseMessage =
                await httpClient.PostAsync($"https://localhost:7126/api/holiday/add", content);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> Delete(int year, int month, int day)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Holiday");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/festivita/delete?year={year}&month={month}&day={day}");

            return httpResponseMessage;
        }

    }

}
