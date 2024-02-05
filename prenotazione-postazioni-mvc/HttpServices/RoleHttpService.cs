using Newtonsoft.Json;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using System.Net.Http;
using System.Text;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class RoleHttpService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl; // Variable to hold the API base URL


        public RoleHttpService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration.GetValue<string>("ApiPath");
        }

        public async Task<HttpResponseMessage> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Role");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/role/getAll");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Role");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/role/getById?idRole={id}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> Update(int idUser, string futureRole)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Role");

            var httpResponseMessage = await httpClient.PutAsync($"{_apiBaseUrl}/api/role/update?idUser={idUser}&futureRole={futureRole}", null);

            return httpResponseMessage;
        }

    }
}
