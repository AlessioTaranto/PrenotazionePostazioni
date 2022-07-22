using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class StanzeHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient httpClient;
        public StanzeHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            this.httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Stanze");
        }
        //getAllStanze
        public async Task<List<Stanza>?> OnGetAllStanze()
        {
            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/stanze/");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadFromJsonAsync<List<Stanza>>();
            }
            throw new HttpRequestException("Errore code: " + httpResponseMessage.StatusCode + " Messaggio: " + httpResponseMessage.RequestMessage);
        }

        public async Task<Stanza?> OnGetStanzaById(int id)
        {
            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/stanze/getStanzaById?id=" + id);
            if (httpResponseMessage.IsSuccessStatusCode)
                return await httpResponseMessage.Content.ReadFromJsonAsync<Stanza>();
            throw new HttpRequestException("Errore code: " + httpResponseMessage.StatusCode + " Messaggio: " + httpResponseMessage.RequestMessage);
        }



        public async Task<Stanza?> OnGetStanzaByName(string name)
        {
            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/stanze/getStanzaByName?stanzaName=" + name);
            if (httpResponseMessage.IsSuccessStatusCode)
                return await httpResponseMessage.Content.ReadFromJsonAsync<Stanza>();
            throw new HttpRequestException("Errore code: " + httpResponseMessage.StatusCode + " Messaggio: " + httpResponseMessage.RequestMessage);
        }
        //public async Task OnPostStanza(StanzaDto stanzaDto)
        //{
        //    return 
        //}
    }
}
