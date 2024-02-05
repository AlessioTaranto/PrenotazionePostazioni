using Newtonsoft.Json;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using System.Text;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class HolidayHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl; // Variable to hold the API base URL


        public HolidayHttpService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration.GetValue<string>("ApiPath");
        }

        public async Task<HttpResponseMessage> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Holiday");

            var httpResponseMessage =
                await httpClient.GetAsync($"{_apiBaseUrl}/api/holiday/getAll");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByDate(int year, int month, int day)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Holiday");

            var httpResponseMessage =
                await httpClient.GetAsync($"{_apiBaseUrl}/api/holiday/getByDate?year={year}&month={month}&day={day}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> Add(DateTime date, string description)
        {
            HolidayDto holidayDto = new HolidayDto(date, description)
            {
                Date = date,
                Description = description
            };

            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Holiday");

            var jsonHoliday = JsonConvert.SerializeObject(holidayDto);
            StringContent content = new StringContent(jsonHoliday, Encoding.UTF8, "application/json");

            var httpResponseMessage = await httpClient.PostAsync("{_apiBaseUrl}/api/holiday/add", content);

            return httpResponseMessage;

            /*var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Holiday");

            string json = "{\"date\": \""+date.ToString("yyyy-MM-ddTHH:mm:ss") +"\", \"desc\": \""+description+"\"}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponseMessage =
                await httpClient.PostAsync($"{_apiBaseUrl}/api/holiday/add", content);

            return httpResponseMessage;*/
        }

        public async Task<HttpResponseMessage> Delete(int year, int month, int day)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Holiday");

            var httpResponseMessage = await httpClient.DeleteAsync($"{_apiBaseUrl}/api/holiday/delete?year={year}&month={month}&day={day}");

            return httpResponseMessage;
        }

    }

}
