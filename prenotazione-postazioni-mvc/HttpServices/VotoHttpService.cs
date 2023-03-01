using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using prenotazione_postazioni_libs.Dto;
using System.Text;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class VotoHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public VotoHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> OnGetVotiFromUtente(int idUtente)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Voti");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/voti/getVotiFromUtente?idUtente={idUtente}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnGetVotiToUtente(int idUtente)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Voti");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/voti/getVotiToUtente?idUtente={idUtente}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnMakeVoto(VotoDto voto)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Voti");
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            

            string json = JsonConvert.SerializeObject(voto); 
            //var content = new StringContent(json);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7126/api/voti/MakeVotoToUtente");
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponseMessage = await httpClient.SendAsync(request);
            
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnDeleteVoto(int idUtente, int idUtenteVotato)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Voti");

            var httpResponseMessage = await httpClient.PostAsync($"https://localhost:7126/api/voti/deleteVoto?idUtente={idUtente}&idUtenteVotato={idUtenteVotato}", null);

            return httpResponseMessage;
        }

        

    }
}
