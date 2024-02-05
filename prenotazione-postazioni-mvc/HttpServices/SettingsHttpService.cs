namespace prenotazione_postazioni_mvc.HttpServices
{
    public class SettingsHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl; // Variable to hold the API base URL


        public SettingsHttpService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration.GetValue<string>("ApiPath");
        }

        public async Task<object> GetModEmergency()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Settings");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/settings/get");

            return httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK ? httpResponseMessage.Content : httpResponseMessage.StatusCode;
        }

    }
}
