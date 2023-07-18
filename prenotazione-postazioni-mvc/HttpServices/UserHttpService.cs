using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using prenotazione_postazioni_libs.Models;
using System.Text;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class UserHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-User");

            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/user/getAll");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetAllWithRole()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-User");

            var httpResponseMessage = await httpClient.GetAsync("https://localhost:7126/api/user/getAllWithRole");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-User");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/user/getById?id={id}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByEmail(string email)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-User");
            email.Replace("\"", "");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/user/getByEmail?email={email}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByDate(DateTime date)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-User");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/user/getByDate?year=" + date.Year + "&month=" + date.Month + "&day=" + date.Day);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> Add(User user)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-User");

            string json = "{" + "\"name\":\"" + user.Name + "\", " + "\"surname\": \"" + user.Surname + "\", " + "\"email\": \"" + user.Email + "\", " + "\"idRole\": " + user.IdRole + "}" + "";
            StringContent ctx = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponseMessage = await httpClient.PostAsync($"https://localhost:7126/api/user/add", ctx);

            return httpResponseMessage;
        }

    }
}
