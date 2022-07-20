namespace prenotazione_postazioni_mvc.Models
{
    public class PresenzeImpostazioniViewModel
    {

        public DateTime PresenzaSelezionata { get; set; }

        public int CollapsedList { get; set; } = 0;

        public PresenzeImpostazioniViewModel()
        {
            PresenzaSelezionata = DateTime.Now;
        }

        public void ToggleCollapseList()
        {
            _ = CollapsedList == 0 ? CollapsedList = 1 : CollapsedList = 0;
        }

    }
}
