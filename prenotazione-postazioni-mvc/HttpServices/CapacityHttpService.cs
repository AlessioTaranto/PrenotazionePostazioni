namespace prenotazione_postazioni_mvc.HttpServices
{
    public class CapacityHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl; // Variable to hold the API base URL

        public CapacityHttpService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration.GetValue<string>("ApiPath");
        }

        public async Task<HttpResponseMessage> GetModEmergency()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Capacity");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/settings/get");

            return httpResponseMessage;
        }
        public async Task<HttpResponseMessage> ToggleModEmergency()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Capacity");

            var httpResponseMessage = await httpClient.PostAsync($"{_apiBaseUrl}/api/settings/update", null);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetAllRooms()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Capacity");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/room/getAll");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> SetCapacity(string room, int capacity)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Capacity");

            var httpResponseMessage = await httpClient.PostAsync($"{_apiBaseUrl}/api/room/updateCapacity?capacity=" + capacity + "&name=" + room, null);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> SetCapacityEmergency(string room, int capacity)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Capacity");

            var httpResponseMessage = await httpClient.PostAsync($"{_apiBaseUrl}/api/room/updateCapacityEmergency?capacity=" + capacity + "&name=" + room, null);

            return httpResponseMessage;
        }

    }
}
