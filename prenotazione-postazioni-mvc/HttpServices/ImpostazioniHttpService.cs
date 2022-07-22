namespace prenotazione_postazioni_mvc.HttpServices
{
    public class ImpostazioniHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ImpostazioniHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<object> OnGetImpostazioniEmergenza()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Impostazioni");

            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/impostazioni/getImpostazioneEmergenza");

            return httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK ? httpResponseMessage.Content : httpResponseMessage.StatusCode;
        }

    }
}
