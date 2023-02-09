namespace prenotazione_postazioni_mvc.HttpServices
{
    public class PrenotazioneHttpSerivice
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public PrenotazioneHttpSerivice(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> OnGetPrenotazioneById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Prenotazioni");

            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/prenotazioni/getPrenotazioneById?idPrenotazione={id}");

            return httpResponseMessage;
        }

    }
}
