namespace prenotazione_postazioni_mvc.Models
{
    public class PrenotazioneViewModel
    {

        public DateTime Date { get; set; }
        public string Stanza { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

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
            Date = new DateTime();
            Stanza = stanza;
            Start = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            End = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
        }

        public PrenotazioneViewModel()
        {
            Date = new DateTime();
            Stanza = "";
            Start = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            End = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
        }
    }
}
