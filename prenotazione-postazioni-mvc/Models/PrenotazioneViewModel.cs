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

        // Stato Collapse (Hour)
        public int CollapsedHour { get; set; } = 0;
        // Stato Collapse (List)
        public int CollapsedList { get; set; } = 0;

        public PrenotazioneViewModel(DateTime date, string stanza, DateTime start, DateTime end)
        {
            Date = date;
            Stanza = stanza;
            Start = start;
            End = end;
        }

        public PrenotazioneViewModel(DateTime date, string stanza)
        {
            Date = date;
            Stanza = stanza;
            Start = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            End = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
        }

        public PrenotazioneViewModel(string stanza)
        {
            Date = DateTime.Now;
            Stanza = stanza;
            Start = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            End = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
        }

        public PrenotazioneViewModel()
        {
            Date = DateTime.Now;
            Start = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            End = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
        }

        /// <summary>
        ///     Ottieni il nome della stanza, se nullo restituisci "Seleziona una stanza"
        /// </summary>
        /// <returns>Stanza != null -> Stanza</returns>
        
        public string GetStanza()
        {
            return this.Stanza == null ? "Seleziona una stanza" : this.Stanza;
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
    }
}
