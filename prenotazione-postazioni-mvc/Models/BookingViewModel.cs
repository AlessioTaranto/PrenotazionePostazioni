using Newtonsoft.Json;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using System.Net;
using System.Xml.Schema;

namespace prenotazione_postazioni_mvc.Models
{
    public class BookingViewModel
    {

        // Giorno selezionato
        public DateTime Date { get; set; }
        // Stanza selezionata
        public string Room { get; set; }

        // Inizio selezionato
        public DateTime StartDate { get; set; }
        // Termine selezionato
        public DateTime EndDate { get; set; }

        public List<User> Attendance { get; set; }

        public int CollapsedHour { get; set; }
        // Stato Collapse (List)
        public int CollapsedList { get; set; }

        // Costante: Minima ora selezionabile
        public const int HourStart = 7;
        // Costante: Massima ora selezionabile
        public const int HourEnd = 22; 

        public BookingHttpSerivice Service { get; set; }

        //Festività selezionata
        public DateTime HolidaySelected { get; set; }

        //Stringa JSON con lista delle feste da leggere in javascript
        public string HolidayJSON { get; set; } = "{}";

        // Http Festa service 
        public HolidayHttpService _holidayHttpService { get; set; }
        public ThemeViewModel _ThemeViewModel = new ThemeViewModel();


        public BookingViewModel(DateTime date, string room, DateTime startDate, DateTime endDate, List<User> attendance, BookingHttpSerivice serivice, HolidayHttpService holidayHttpService, ThemeViewModel themeViewModel)
        {
            Date = date;
            Room = room;
            StartDate = startDate;
            EndDate = endDate;
            Attendance = attendance;
            Service = serivice;
            _holidayHttpService = holidayHttpService;
            _ThemeViewModel = themeViewModel;
        }

        public BookingViewModel(DateTime date, string room, BookingHttpSerivice serivice, HolidayHttpService holidayHttpService)
        {
            Date = date;
            Room = room;
            StartDate = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            EndDate = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
            Attendance = new List<User>();
            Service = serivice;
            _holidayHttpService = holidayHttpService;
        }

        public BookingViewModel(string room, BookingHttpSerivice serivice, HolidayHttpService holidayHttpService)
        {
            Date = DateTime.Now;
            Room = room;
            StartDate = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            EndDate = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
            Attendance = new List<User>();
            Service = serivice;
            _holidayHttpService = holidayHttpService;
        }

        public BookingViewModel(BookingHttpSerivice serivice, HolidayHttpService holidayHttpService)
        {
            Room = "null";
            Date = DateTime.Now;
            StartDate = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            EndDate = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
            Attendance = new List<User>();
            Service = serivice;
            _holidayHttpService = holidayHttpService;
        }

        /// <summary>
        ///     Ottieni il nome della stanza, se nullo restituisci "Seleziona una stanza"
        /// </summary>
        /// <returns>Stanza != null -> Stanza</returns>
        
        public string GetRoom()
        {
            return this.Room == "null" ? "Seleziona una stanza" : this.Room;
        }

        /// <summary>
        ///     Adatta l'orario selezionato al formato del datepicker HTML:
        ///     Se l'ora selezionata ha solo un carattere, aggiunge uno 0 davanti.
        /// </summary>
        /// <param name="number">Ora selezionata</param>
        /// <returns>Ora selezionata formattata</returns>
        
        public string FormatHour(int number)
        {
            return number.ToString().Length == 1 ? ("0" + number.ToString()) : number.ToString();
        }

        /// <summary>
        ///     Abilita / Disabilita Collapse Hour
        /// </summary>
        
        public void ToggleCollapseHour()
        {
            _ = CollapsedHour == 0 ? CollapsedHour = 1 : CollapsedHour = 0;
        }

        /// <summary>
        ///     Abilita / Disabilita Collapse List
        /// </summary>

        public void ToggleCollapseList()
        {
            _ = CollapsedList == 0 ? CollapsedList = 1 : CollapsedList = 0;
        }

        /// <summary>
        ///     Cambia il giorno selezionato e valida il nuovo giorno
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <exception cref="Exception">Giorno precedente a quello odierno, Giorno non valido</exception>

        public void ChangeSelectedDay(int year, int month, int day)
        {

            DateTime nowDate = DateTime.Now;

            if ((year < nowDate.Year) || (month < nowDate.Month && year == nowDate.Year) || (day < nowDate.Day && month == nowDate.Month && year == nowDate.Year))
                throw new Exception("Non puoi selezionare una data precedente alla data corrente");

            try
            {
                Date = new DateTime(year, month, day);
                StartDate = new DateTime(year, month, day, StartDate.Hour, 0, 0);
                EndDate = new DateTime(year, month, day, EndDate.Hour, 0, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("La data selezionata non è valida");
            } 

        }

        /// <summary>
        ///     Cambia l'orario di inizio, e valida il nuovo orario
        /// </summary>
        /// <param name="hour">Orario selezionato</param>
        /// <exception cref="Exception">Ora precedente minore del minimo, Ora maggiore del massimo, Orario successivo al termine, Orario non valido</exception>

        public void ChangeSelectedStartHour(int hour)
        {
            if (hour < HourStart)
                throw new Exception("Non puoi selezionare un orario prima delle " + HourStart);

            if (hour > HourEnd)
                throw new Exception("Non puoi selezionare un orario dopo delle " + HourEnd);

            if (hour > EndDate.Hour)
                throw new Exception("Non puoi selezionare un orario d'inizio successivo a quello di termine");

            try
            {
                StartDate = new DateTime(Date.Year, Date.Month, Date.Day, hour, 0, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("L'orario selezionato non è valido");
            }

        }

        /// <summary>
        ///     Cambia l'orario di termine, e valida il nuovo orario
        /// </summary>
        /// <param name="hour">Orario selezionato</param>
        /// <exception cref="Exception">Ora precedente minore del minimo, Ora maggiore del massimo, Orario precedente all'inizio, Orario non valido</exception>


        public void ChangeSelectedEndHour(int hour)
        {
            if (hour < HourStart)
                throw new Exception("Non puoi selezionare un orario prima delle " + HourStart);

            if (hour > HourEnd)
                throw new Exception("Non puoi selezionare un orario dopo delle " + HourEnd);

            if (hour < StartDate.Hour)
                throw new Exception("Non puoi selezionare un orario di termine precedente a quello d'inizio");

            try
            {
                EndDate = new DateTime(Date.Year, Date.Month, Date.Day, hour, 0, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("L'orario selezionato non è valido");
            }

        }

        /// <summary>
        /// Crea una prenotazione
        /// </summary>
        /// <param name="utente"></param>
        /// <param name="stanza"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task DoBookingAsync(string utente, string stanza, string start, string end)
        {
            HttpResponseMessage status = await Service.Add(start, end, utente, stanza);
        }

        public async Task<HttpStatusCode> ExistBooking(string userParam, string roomParam, string start, string end)
        {

            if (roomParam == null || userParam == null || start == null || end == null)
                return HttpStatusCode.UnprocessableEntity;

            //Translating json text to objects
            User? user = JsonConvert.DeserializeObject<User>(userParam);
            Room? room = JsonConvert.DeserializeObject<Room>(roomParam);
            DateTime startDate = JsonConvert.DeserializeObject<DateTime>(start);
            DateTime endDate = JsonConvert.DeserializeObject<DateTime>(end);

            if (room == null || user == null)
                return HttpStatusCode.UnprocessableEntity;

            HttpResponseMessage msgRq = await Service.GetByDate(room.Id, startDate, endDate);

            if (msgRq != null && msgRq.StatusCode == System.Net.HttpStatusCode.OK)
            {

                List<Booking>? bookings = await msgRq.Content.ReadFromJsonAsync<List<Booking>?>();

                foreach (Booking booking in bookings)
                    if (booking.IdUser == user.Id)
                        return HttpStatusCode.OK;

                return HttpStatusCode.NotFound;

            }

            return HttpStatusCode.UnprocessableEntity;
        }

        public async Task<Booking?> GetBooking(string userParam, string roomParam, string start, string end)
        {

            if (roomParam == null || userParam == null || start == null || end == null)
                return null;

            //Translating json text to objects
            User? user = JsonConvert.DeserializeObject<User>(userParam);
            Room? room = JsonConvert.DeserializeObject<Room>(roomParam);
            DateTime inizio = JsonConvert.DeserializeObject<DateTime>(start);
            DateTime fine = JsonConvert.DeserializeObject<DateTime>(end);

            if (room == null || user == null)
                return null;

            HttpResponseMessage msgRq = await Service.GetByDate(room.Id, inizio, fine);

            if (msgRq != null && msgRq.StatusCode == System.Net.HttpStatusCode.OK)
            {

                List<Booking>? bookings = await msgRq.Content.ReadFromJsonAsync<List<Booking>?>();

                foreach (Booking booking in bookings)
                    if (booking.IdUser == user.Id)
                        return booking;

                return null;

            }

            return null;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            //Continua qua
            HttpResponseMessage deleteRq = await Service.Delete(id);

            return deleteRq;
        }

        public async Task ReloadHoliday()
        {
            HttpResponseMessage msg = await _holidayHttpService.GetAll();
            if (msg == null || msg.StatusCode != HttpStatusCode.OK)
                return;

            Task<String> ctxString = msg.Content.ReadAsStringAsync();
            ctxString.Wait();
            HolidayJSON = ctxString.Result;
        }
    }
}
