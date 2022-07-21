namespace prenotazione_postazioni_mvc.Models
{
    public class PresenzeImpostazioniViewModel
    {
        // Giorno selezionato
        public DateTime PresenzaSelezionata { get; set; }

        // Stato del Collapse (List)
        public int CollapsedList { get; set; }

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

        /// <summary>
        ///     Seleziona un nuovo giorno di Presenza, e valida il giorno
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <exception cref="Exception">Giorno non valido</exception>

        public void SelectPresenza(int year, int month, int day)
        {
            try
            {
                PresenzaSelezionata = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Giorno non valido");
            }
        }

    }
}
