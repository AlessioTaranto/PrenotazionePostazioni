using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class RoomHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RoomHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Room");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/room/getAll");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Room");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/room/getById?id={id}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByName(string name)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Room");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/room/getByName?name={name}");

            return httpResponseMessage;
        }
    }
}
