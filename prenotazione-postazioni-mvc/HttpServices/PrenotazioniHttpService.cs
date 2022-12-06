using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using System.Text;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class PrenotazioniHttpService : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PrenotazioniHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<object> OnGetImpostazioniEmergenza()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Impostazioni");

            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/impostazioni/getImpostazioneEmergenza");

            return httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK ? httpResponseMessage.Content : httpResponseMessage.StatusCode;
        }

        public async Task<HttpResponseMessage> OnGetUtentiPrenotatiByDay(DateTime date)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Impostazioni");

            string string_data = date.ToString("yyyy-MM-dd HH-mm-ss");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/impostazioni/getUtentiPrenotatiByDay?date={string_data}");

            return httpResponseMessage;
        }

        //getPrenotazioniById
        public async Task<Prenotazione?> OnGetPrenotazioneById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Prenotazioni");
            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/prenotazioni/getPrenotazioniById?idPrenotazione={id}");
            Prenotazione? prenotazione = await httpResponseMessage.Content.ReadFromJsonAsync<Prenotazione>();
            return prenotazione;
        }

        //getAllPrenotazioni
        public async Task<List<Prenotazione>?> OnGetAllPrenotazioni()
        {
            var httpclient = _httpClientFactory.CreateClient("PrenotazionePostazione-Prenotazioni");
            var httpResponseMessage = await httpclient.GetAsync("https://localhost:7126/api/prenotazioni/getAllPrenotazioni");
            List<Prenotazione>? prenotazioni = await httpResponseMessage.Content.ReadFromJsonAsync<List<Prenotazione>>();
            return prenotazioni;
        }

        //getPrenotazioniByStanza
        public async Task<List<Prenotazione>?> OnGetAllPrenotazioniByIdStanza(int idStanza)
        {
            var httpclient = _httpClientFactory.CreateClient("PrenotazionePostazione-Prenotazioni");
            var httpResponseMessage = await httpclient.GetAsync("https://localhost:7126/api/prenotazioni/getPrenotazioniByStanza?idStanza=" + idStanza);
            List<Prenotazione>? prenotazioni = await httpResponseMessage.Content.ReadFromJsonAsync<List<Prenotazione>>();
            return prenotazioni;
        }

        //getPrenotazioniByUtente
        public async Task<List<Prenotazione>?> OnGetAllPrenotazioniByIdUtente(int idUtente)
        {
            var httpclient = _httpClientFactory.CreateClient("PrenotazionePostazione-Prenotazioni");
            var httpResponseMessage = await httpclient.GetAsync("https://localhost:7126/api/prenotazioni/getPrenotazioniByUtente?idUtente=" + idUtente);
            List<Prenotazione>? prenotazioni = await httpResponseMessage.Content.ReadFromJsonAsync<List<Prenotazione>>();
            return prenotazioni;
        }

        //getPrenotazioniByDate
        public async Task<List<Prenotazione>?> OnGetAllPrenotazioniByDate(int idStanza, DateTime start, DateTime end)
        {
            var httpclient = _httpClientFactory.CreateClient("PrenotazionePostazione-Prenotazioni");
            var httpResponseMessage = await httpclient.GetAsync($"https://localhost:7126/api/prenotazioni/getPrenotazioniByDate?idStanza={idStanza}&startDate={start}&endDate={end}");
            List<Prenotazione>? prenotazioni = await httpResponseMessage.Content.ReadFromJsonAsync<List<Prenotazione>>();
            return prenotazioni;
        }

        //
        public async Task OnPostPrenotazione(PrenotazioneDto prenotazioneDto)
        {
            var httpclient = _httpClientFactory.CreateClient("PrenotazionePostazione-Prenotazioni");
            var jsonStringContent = new StringContent(JsonConvert.SerializeObject(prenotazioneDto), Encoding.UTF8, "application/json");
            await httpclient.PostAsync("https://localhost:7126/api/prenotazioni/addPrenotazione", jsonStringContent);
        }

    }
}
