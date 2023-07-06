using Newtonsoft.Json;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using System.Text;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class BookingHttpSerivice
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public BookingHttpSerivice(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Booking");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/booking/getById?id={id}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Booking");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/booking/getAll");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByRoom(int idRoom)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Booking");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/booking/getByRoom?idRoom={idRoom}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByUser(int idUser)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Booking");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/booking/getByUser?idUser={idUser}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByDate(int idRoom, DateTime start, DateTime end)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Booking");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/booking/getByDate?idRoom={idRoom}&startDateYear={start.Year}&startDateMonth={start.Month}&startDateDay={start.Day}&startDateHour={start.Hour}&startDateMinute={start.Minute}&endDateYear={end.Year}&endDateMonth={end.Month}&endDateDay={end.Day}&endDateHour={end.Hour}&endDateMinute={end.Minute}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> Add(string start, string end, string utente, string room)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Booking");


            string jsonBooking = "{\"startDate\": " + start + "," + "\"endDate\": " + end + "," + "\"room\": " + room + "," + "\"user\": " + utente + "}" + "";
            StringContent ctx = new StringContent(jsonBooking, Encoding.UTF8, "application/json");

            var httpResponseMessage = await httpClient.PostAsync($"https://localhost:7126/api/booking/add", ctx);

            //Controllo esistenza prenotazione

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Booking");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/booking/delete?id={id}");

            return httpResponseMessage;
        }

    }
}
