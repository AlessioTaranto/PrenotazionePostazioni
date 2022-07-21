namespace prenotazione_postazioni_mvc.Models
{
    public class CapienzaImpostazioniViewModel
    {

        // Stanza selezionata
        public string Stanza { get; set; }
        // Capienza selezionata
        public int Capienza { get; set; }

        
        public CapienzaImpostazioniViewModel(string stanza, int capienza)
        {
            Stanza = stanza;
            Capienza = capienza;
        }

        public CapienzaImpostazioniViewModel(string stanza)
        {
            Stanza = stanza;
            Capienza = 5;
        }

        public CapienzaImpostazioniViewModel()
        {
            Stanza = "null";
            Capienza = 5;
        }

        /// <summary>
        ///     Valida i dati inseriti da un utente
        /// </summary>
        /// <exception cref="Exception">Stanza non selezionata, Capienza non valida</exception>
        public void Validate()
        {
            if (Stanza == null)
                throw new Exception("Seleziona una stanza");

            if (Capienza <= 0)
                throw new Exception("Capienza non valida");
        }

        /// <summary>
        ///     Formatta la visualizzazione della stanza nella Div a destra
        /// </summary>
        /// <returns>Se è stata selezionata una stanza, ritorna la stanza, altrimenti "Seleziona una stanzas"</returns>
        public string GetStanza()
        {
            return Stanza == "null" ? "Seleziona una stanza" : Stanza;
        }
    }
}
