namespace prenotazione_postazioni_mvc.HttpServices
{
    public class SettingsHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SettingsHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<object> GetModEmergency()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Settings");

            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/settings/get");

            return httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK ? httpResponseMessage.Content : httpResponseMessage.StatusCode;
        }

    }
}
