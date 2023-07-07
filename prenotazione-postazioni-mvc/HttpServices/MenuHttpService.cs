using Microsoft.VisualBasic;
using System.Text;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class MenuHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MenuHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<HttpResponseMessage> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Menu");
            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/menu/getAll");
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetMenu(int year, int month, int day)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Menu");
            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/menu/getByDate?year={year}&month={month}&day{day}");
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetMenu(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Menu");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/menu/getById?id={id}");

            return httpResponseMessage;
        }
        public async Task<HttpResponseMessage> AddMenu(DateOnly day, string image)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Menu");

            string json = "{\"day\": \"" + day.ToString("yyyy-MM-ddTHH:mm:ss") + "\", \"image\": \"" + image + "\"}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponseMessage =
                await httpClient.PostAsync($"https://localhost:7126/api/menu/addMenu", content);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> DeleteMenu(int year, int month, int day)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Menu");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/menu/deleteMenu?year={year}&month={month}&day={day}");

            return httpResponseMessage;
        }
    }
}
