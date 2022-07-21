namespace prenotazione_postazioni_mvc.Models
{
    public class PresenzeImpostazioniViewModel
    {
        // Giorno selezionato
        public DateTime PresenzaSelezionata { get; set; }

        // Stato del Collapse (List)
        public int CollapsedList { get; set; } = 0;

        public PresenzeImpostazioniViewModel()
        {
            PresenzaSelezionata = DateTime.Now;
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
