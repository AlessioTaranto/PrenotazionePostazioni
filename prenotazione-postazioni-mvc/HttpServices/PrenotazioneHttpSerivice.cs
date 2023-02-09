using Newtonsoft.Json;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using System.Text;

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

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/prenotazioni/getPrenotazioneById?idPrenotazione={id}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnGetAllPrenotazioni()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Prenotazioni");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/prenotazioni/getAllPrenotazioni");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnGetPrenotazioneByStanza(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Prenotazioni");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/prenotazioni/getPrenotazioniByStanza?idStanza={id}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnGetPrenotazioneByUtente(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Prenotazioni");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/prenotazioni/getPrenotazioniByUtente?idUtente={id}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnGetPrenotazioneByDate(int idStanza, DateTime start, DateTime end)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Prenotazioni");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/prenotazioni/getPrenotazioniByDate?idStanza={idStanza}&startDateYear={start.Year}&startDateMonth={start.Month}&startDateDay={start.Day}&startDateHour={start.Hour}&startDateMinute={start.Minute}&endDateYear={end.Year}&endDateMonth={end.Month}&endDateDay={end.Day}&endDateHour={end.Hour}&endDateMinute={end.Minute}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> AddPrenotazione(string start, string end, string utente, string stanza)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Postazioni");


            string jsonPrenotazione = "{\"startDate\": " + start + "," + "\"endDate\": " + end + "," + "\"stanza\": " + stanza + "," + "\"utente\": " + utente + "}" + "";
            StringContent ctx = new StringContent(jsonPrenotazione, Encoding.UTF8, "application/json");

            var httpResponseMessage = await httpClient.PostAsync($"https://localhost:7126/api/prenotazioni/addPrenotazione", ctx);

            //Controllo esistenza prenotazione

            return httpResponseMessage;
        }

    }
}
