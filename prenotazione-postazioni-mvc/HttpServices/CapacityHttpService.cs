namespace prenotazione_postazioni_mvc.HttpServices
{
    public class CapacityHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CapacityHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetModEmergency()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Capacity");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/settings/get");

            return httpResponseMessage;
        }
        public async Task<HttpResponseMessage> ToggleModEmergency()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Capacity");

            var httpResponseMessage = await httpClient.PostAsync($"https://localhost:7126/api/settings/update", null);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetAllRooms()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Capacity");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/room/getAll");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> SetCapacity(string room, int capacity)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Capacity");

            var httpResponseMessage = await httpClient.PostAsync($"https://localhost:7126/api/room/updateCapacity?postiMax=" + capacity + "&name=" + room, null);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> SetCapacityEmergency(string room, int capacity)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Capacity");

            var httpResponseMessage = await httpClient.PostAsync($"https://localhost:7126/api/stanze/changePostiMaxEmergenza?postiMax=" + capacity + "&name=" + room, null);

            return httpResponseMessage;
        }

    }
}
