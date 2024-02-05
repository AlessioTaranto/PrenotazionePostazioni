using Newtonsoft.Json;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using System.Net.Http;
using System.Text;

namespace prenotazione_postazioni_mvc.HttpServices
{
    public class BookingHttpSerivice
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl; // Variable to hold the API base URL


        public BookingHttpSerivice(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration.GetValue<string>("ApiPath");
        }

        public async Task<HttpResponseMessage> GetById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Booking");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/booking/getById?id={id}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Booking");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/booking/getAll");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByRoom(int idRoom)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Booking");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/booking/getByRoom?idRoom={idRoom}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByUser(int idUser)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Booking");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/booking/getByUser?idUser={idUser}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByDate(int idRoom, DateTime start, DateTime end)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Booking");

            var httpResponseMessage = await httpClient.GetAsync($"{_apiBaseUrl}/api/booking/getByDate?idRoom={idRoom}&startDateYear={start.Year}&startDateMonth={start.Month}&startDateDay={start.Day}&startDateHour={start.Hour}&startDateMinute={start.Minute}&endDateYear={end.Year}&endDateMonth={end.Month}&endDateDay={end.Day}&endDateHour={end.Hour}&endDateMinute={end.Minute}");

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> Add(DateTime start, DateTime end, User utente, Room room)
        {
            BookingDto bookingDto = new BookingDto(start, end, room.Id, utente.Id)
            {
                StartDate = start,
                EndDate = end,
                IdUser = utente.Id,
                IdRoom = room.Id
            };

            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-Booking");

            var jsonBooking = JsonConvert.SerializeObject(bookingDto);
            StringContent content = new StringContent(jsonBooking, Encoding.UTF8, "application/json");

            var httpResponseMessage = await httpClient.PostAsync("{_apiBaseUrl}/api/booking/add", content);

            return httpResponseMessage;
        }


        public async Task<HttpResponseMessage> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-Booking");

            var httpResponseMessage = await httpClient.DeleteAsync($"{_apiBaseUrl}/api/booking/delete?id={id}");

            return httpResponseMessage;
        }

    }
}
