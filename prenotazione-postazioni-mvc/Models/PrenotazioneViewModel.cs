using Newtonsoft.Json;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using System.Net;
using System.Xml.Schema;

namespace prenotazione_postazioni_mvc.Models
{
    public class PrenotazioneViewModel
    {

        // Giorno selezionato
        public DateTime Date { get; set; }
        // Stanza selezionata
        public string Stanza { get; set; }

        // Inizio selezionato
        public DateTime Start { get; set; }
        // Termine selezionato
        public DateTime End { get; set; }

        public List<Utente> Presenti { get; set; }

        public int CollapsedHour { get; set; }
        // Stato Collapse (List)
        public int CollapsedList { get; set; }

        // Costante: Minima ora selezionabile
        public const int HourStart = 7;
        // Costante: Massima ora selezionabile
        public const int HourEnd = 22;

        public PrenotazioneHttpSerivice Service { get; set; }

        public PrenotazioneViewModel(DateTime date, string stanza, DateTime start, DateTime end, List<Utente> presenti, PrenotazioneHttpSerivice serivice)
        {
            Date = date;
            Stanza = stanza;
            Start = start;
            End = end;
            Presenti = presenti;
            Service = serivice;
        }

        public PrenotazioneViewModel(DateTime date, string stanza, PrenotazioneHttpSerivice serivice)
        {
            Date = date;
            Stanza = stanza;
            Start = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            End = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
            Presenti = new List<Utente>();
            Service = serivice;
        }

        public PrenotazioneViewModel(string stanza, PrenotazioneHttpSerivice serivice)
        {
            Date = DateTime.Now;
            Stanza = stanza;
            Start = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            End = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
            Presenti = new List<Utente>();
            Service = serivice;
        }

        public PrenotazioneViewModel(PrenotazioneHttpSerivice serivice)
        {
            Stanza = "null";
            Date = DateTime.Now;
            Start = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            End = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
            Presenti = new List<Utente>();
            Service = serivice;
        }

        /// <summary>
        ///     Ottieni il nome della stanza, se nullo restituisci "Seleziona una stanza"
        /// </summary>
        /// <returns>Stanza != null -> Stanza</returns>
        
        public string GetStanza()
        {
            return this.Stanza == "null" ? "Seleziona una stanza" : this.Stanza;
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
                Start = new DateTime(year, month, day, Start.Hour, 0, 0);
                End = new DateTime(year, month, day, End.Hour, 0, 0);
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

            if (hour > End.Hour)
                throw new Exception("Non puoi selezionare un orario d'inizio successivo a quello di termine");

            try
            {
                Start = new DateTime(Date.Year, Date.Month, Date.Day, hour, 0, 0);
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

            if (hour < Start.Hour)
                throw new Exception("Non puoi selezionare un orario di termine precedente a quello d'inizio");

            try
            {
                End = new DateTime(Date.Year, Date.Month, Date.Day, hour, 0, 0);
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
        public async Task doPrenotazioneAsync(string utente, string stanza, string start, string end)
        {
            HttpResponseMessage status = await Service.AddPrenotazione(start, end, utente, stanza);
        }

        public async Task<HttpStatusCode> ExistsPrenotazione(string utente, string stanza, string start, string end)
        {

            if (stanza == null || utente == null || start == null || end == null)
                return HttpStatusCode.UnprocessableEntity;

            //Translating json text to objects
            Utente? user = JsonConvert.DeserializeObject<Utente>(utente);
            Stanza? room = JsonConvert.DeserializeObject<Stanza>(stanza);
            DateTime inizio = JsonConvert.DeserializeObject<DateTime>(start);
            DateTime fine = JsonConvert.DeserializeObject<DateTime>(end);

            if (room == null || user == null)
                return HttpStatusCode.UnprocessableEntity;

            HttpResponseMessage msgRq = await Service.OnGetPrenotazioneByDate(room.IdStanza, inizio, fine);

            if (msgRq != null && msgRq.StatusCode == System.Net.HttpStatusCode.OK)
            {

                List<Prenotazione>? prenotazioni = await msgRq.Content.ReadFromJsonAsync<List<Prenotazione>?>();

                foreach (Prenotazione prenotazione in prenotazioni)
                    if (prenotazione.IdUtente == user.IdUtente)
                        return HttpStatusCode.OK;

                return HttpStatusCode.NotFound;

            }

            return HttpStatusCode.UnprocessableEntity;
        }

        public async Task<Prenotazione?> GetPrenotazione(string utente, string stanza, string start, string end)
        {

            if (stanza == null || utente == null || start == null || end == null)
                return null;

            //Translating json text to objects
            Utente? user = JsonConvert.DeserializeObject<Utente>(utente);
            Stanza? room = JsonConvert.DeserializeObject<Stanza>(stanza);
            DateTime inizio = JsonConvert.DeserializeObject<DateTime>(start);
            DateTime fine = JsonConvert.DeserializeObject<DateTime>(end);

            if (room == null || user == null)
                return null;

            HttpResponseMessage msgRq = await Service.OnGetPrenotazioneByDate(room.IdStanza, inizio, fine);

            if (msgRq != null && msgRq.StatusCode == System.Net.HttpStatusCode.OK)
            {

                List<Prenotazione>? prenotazioni = await msgRq.Content.ReadFromJsonAsync<List<Prenotazione>?>();

                foreach (Prenotazione prenotazione in prenotazioni)
                    if (prenotazione.IdUtente == user.IdUtente)
                        return prenotazione;

                return null;

            }

            return null;
        }

        public async Task<HttpResponseMessage> DeletePrenotazione(int idPrenotazione)
        {
            //Continua qua
            HttpResponseMessage deleteRq = await Service.DeletePrenotazione(idPrenotazione);

            return deleteRq;
        }

    }
}
