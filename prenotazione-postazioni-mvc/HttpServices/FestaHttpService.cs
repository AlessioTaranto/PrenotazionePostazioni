namespace prenotazione_postazioni_mvc.HttpServices
{
    public class FestaHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FestaHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> getCovidMode()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Capienza");

            var httpResponseMessage =
                await httpClient.GetAsync($"https://localhost:7126/api/impostazioni/getImpostazioneEmergenza");

            return httpResponseMessage;
        }

    }

}
