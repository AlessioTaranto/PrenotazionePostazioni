namespace prenotazione_postazioni_mvc.HttpServices
{
    public class CapienzaHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CapienzaHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> getCovidMode()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Capienza");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/impostazioni/getImpostazioneEmergenza");

            return httpResponseMessage;
        }
        public async Task<HttpResponseMessage> ToggleCovidMode()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Capienza");

            var httpResponseMessage = await httpClient.PostAsync($"https://localhost:7126/api/impostazioni/changeImpostazioneEmergenza", null);

            return httpResponseMessage;
        }

    }
}
