using Microsoft.VisualBasic;
using Newtonsoft.Json;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class MenuHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MenuHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<HttpResponseMessage> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Menu");
            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/menu/getAll");
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByDate(int year, int month, int day)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Menu");
            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/menu/getByDate?year={year}&month={month}&date={day}");
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Menu");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/menu/getById?id={id}");

            return httpResponseMessage;
        }
        public async Task<HttpResponseMessage> Add(DateTime date, byte[] image)
        {
            MenuDto menuDto = new MenuDto(date, image)
            {
                Date = date,
                MenuImage = image
            };

            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Menu");

            var jsonMenu = JsonConvert.SerializeObject(menuDto);
            StringContent content = new StringContent(jsonMenu, Encoding.UTF8, "application/json");

            var httpResponseMessage = await httpClient.PostAsync("https://localhost:7126/api/menu/add", content);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> Delete(int year, int month, int day)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Menu");

            var httpResponseMessage = await httpClient.DeleteAsync($"https://localhost:7126/api/menu/delete?year={year}&month={month}&day={day}");

            return httpResponseMessage;
        }
    }
}
