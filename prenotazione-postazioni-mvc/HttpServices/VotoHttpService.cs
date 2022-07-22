using Newtonsoft.Json;
using prenotazione_postazioni_libs.Dto;

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

        public async Task<HttpResponseMessage> OnMakeVoto(VotoDto voto)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Voti");

            string json = JsonConvert.SerializeObject(voto); 
            var content = new StringContent(json);

            var httpResponseMessage = await httpClient.PostAsync("https://localhost:7126/api/voti/MakeVotoToUtente",content);
            
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnDeleteVoto(int idUtente, int idUtenteVotato)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Voti");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/voti/deleteVoto?idUtente={idUtente}&idUtenteVotato={idUtenteVotato}");

            return httpResponseMessage;
        }

    }
}
