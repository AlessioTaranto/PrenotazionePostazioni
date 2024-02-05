using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using prenotazione_postazioni_libs.Dto;
using System.Text;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class VoteHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl; // Variable to hold the API base URL

        public VoteHttpService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration.GetValue<string>("ApiPath");
        }

        public async Task<HttpResponseMessage> GetUserVotes(int idUser)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Vote");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/vote/getUserVotes?idUser={idUser}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetVictimVotes(int idVictim)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Vote");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/vote/getVictimVotes?idVictim={idVictim}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> Add(VoteDto vote)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Vote");
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            

            string json = JsonConvert.SerializeObject(vote); 
            //var content = new StringContent(json);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{_apiBaseUrl}/api/vote/add");
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponseMessage = await httpClient.SendAsync(request);
            
            return httpResponseMessage;
        }

       /*  public async Task<HttpResponseMessage> Delete(int idUser, int idVictim)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Vote");

            var httpResponseMessage = await httpClient.DeleteAsync($"{_apiBaseUrl}/api/vote/delete?idUser={idUser}&idVictim={idVictim}");

            return httpResponseMessage;
        }
 */
        

    }
}
