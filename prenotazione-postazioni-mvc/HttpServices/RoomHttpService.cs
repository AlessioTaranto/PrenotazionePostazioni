using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class RoomHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl; // Variable to hold the API base URL


        public RoomHttpService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration.GetValue<string>("ApiPath");
        }

        public async Task<HttpResponseMessage> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Room");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/room/getAll");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Room");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/room/getById?id={id}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByName(string name)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Room");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/room/getByName?name={name}");

            return httpResponseMessage;
        }
    }
}
