using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using prenotazione_postazioni_libs.Models;
using System.Text;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class UtenteHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UtenteHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> OnGetAllUtenti()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Utente");

            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/utenti/getAllUtenti");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnGetUtenteById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Utente");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/utenti/getUtenteById?id={id}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnGetUtenteByEmail(string email)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Utente");
            email.Replace("\"", "");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/utenti/getUtenteByEmail?email={email}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> OnGetUtentiPrenotatiByDay(DateTime date)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Utente");

            var year = date.Year;
            var month = date.Month;
            var day = date.Day;

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/utenti/getUtentiPrenotatiByDay?year={year}&month={month}&day={day}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> AddNewUser(User user)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Utente");

            string json = "{" + "\"nome\":\"" + user.Name + "\", " + "\"cognome\": \"" + user.Surname + "\", " + "\"image\": \"" + user.Image + "\", " + "\"email\": \"" + user.Email + "\", " + "\"idRuolo\": " + user.IdRole + "}" + "";
            StringContent ctx = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponseMessage = await httpClient.PostAsync($"https://localhost:7126/api/utenti/addNewUtente", ctx);

            return httpResponseMessage;
        }

    }
}
