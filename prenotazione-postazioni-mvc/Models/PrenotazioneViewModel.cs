using prenotazione_postazioni_libs.Models;
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

        public PrenotazioneViewModel(DateTime date, string stanza, DateTime start, DateTime end, List<Utente> presenti)
        {
            Date = date;
            Stanza = stanza;
            Start = start;
            End = end;
            Presenti = presenti;
        }

        public PrenotazioneViewModel(DateTime date, string stanza)
        {
            Date = date;
            Stanza = stanza;
            Start = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            End = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
            Presenti = new List<Utente>();
        }

        public PrenotazioneViewModel(string stanza)
        {
            Date = DateTime.Now;
            Stanza = stanza;
            Start = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            End = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
            Presenti = new List<Utente>();
        }

        public PrenotazioneViewModel()
        {
            Stanza = "null";
            Date = DateTime.Now;
            Start = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            End = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
            Presenti = new List<Utente>();
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

    }
}
