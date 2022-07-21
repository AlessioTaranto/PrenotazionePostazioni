namespace prenotazione_postazioni_mvc.Models
{
    public class FestivitaImpostazioniViewModel
    {

        // Giorno selezionato
        public DateTime GiornoSelezionato { get; set; }
        // Lista di tutte le festività
        public List<DateTime> Festivita { get; set; }

        public FestivitaImpostazioniViewModel(DateTime giornoSelezionato, List<DateTime> festivita)
        {
            GiornoSelezionato = giornoSelezionato;
            Festivita = festivita;
        }

        public FestivitaImpostazioniViewModel(List<DateTime> festivita)
        {
            Festivita = festivita;
        }

        public FestivitaImpostazioniViewModel()
        {
            Festivita = new List<DateTime>();
            //Query API che prende le feste dal db
        }

        /// <summary>
        ///     Aggiungi una festività alla lista delle Festività
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <exception cref="Exception">Festività già inserita</exception>

        public void AddFesta(int year, int month, int day)
        {
            DateTime date = new DateTime(year, month, day);

            if (Festivita.Contains(date))
                throw new Exception("Festività già inserita");

            Festivita.Add(date);
        }

        /// <summary>
        ///     Rimuovi una festività dalla lista delle Festività
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <exception cref="Exception">Festività non inserita</exception>

        public void RemoveFesta(int year, int month, int day)
        {
            DateTime date = new DateTime(year, month, day);

            if (Festivita.Contains(date))
                throw new Exception("Festività non presente");

            Festivita.Remove(date);
        }

        /// <summary>
        ///     Formatta la visualizzazione del "Giorno selezionato" della Div a destra
        /// </summary>
        /// <returns>Se è stato selezionato un giorno ritorna il "Giorno selezionato: giorno" altrimenti "Seleziona un giorno"</returns>

        public string GetFestaSelezionata()
        {
            //  Se l'anno del giorno selezionato è 1, allora non è stato selezionato
            return GiornoSelezionato.Year == 1 ? "Seleziona un giorno" : "Giorno selezionato: "+GiornoSelezionato.Day+"/"+GiornoSelezionato.Month+"/"+GiornoSelezionato.Year;
        }
    }
}
