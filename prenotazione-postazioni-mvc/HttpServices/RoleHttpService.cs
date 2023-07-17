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

        public RoleHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Role");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/role/getAll");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> Update(int idUser, string futureRole)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Role");

            var httpResponseMessage = await httpClient.PutAsync($"https://localhost:7126/api/role/update?idUser={idUser}&futureRole={futureRole}", null);

            return httpResponseMessage;
        }

    }
}
