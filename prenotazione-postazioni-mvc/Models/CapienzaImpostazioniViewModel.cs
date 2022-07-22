using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace prenotazione_postazioni_mvc.Models
{
    public class CapienzaImpostazioniViewModel
    {

        // Stanza selezionata
        public string? Stanza { get; set; }

        // Modalità Covid
        public bool CovidMode { get; set; }

        // Lista capienza normale di tutte le stanze
        public Dictionary<string, int>? CapienzaNormale { get; set; }

        // Lista capienza covid di tutte le stanze
        public Dictionary<string, int>? CapienzaCovid { get; set; }

        public CapienzaImpostazioniViewModel(string? stanza, bool covidMode, Dictionary<string, int>? capienzaNormale, Dictionary<string, int>? capienzaCovid)
        {
            Stanza = stanza;
            CovidMode = covidMode;
            CapienzaNormale = capienzaNormale;
            CapienzaCovid = capienzaCovid;
        }

        public CapienzaImpostazioniViewModel(string? stanza, bool covidMode)
        {
            Stanza = stanza;
            CovidMode = covidMode;
            LoadCapienzaCovid();
            LoadCapienzaNormale();
        }

        public CapienzaImpostazioniViewModel(bool covidMode)
        {
            Stanza = "null";
            CovidMode = covidMode;
            LoadCapienzaCovid();
            LoadCapienzaNormale();
        }

        public CapienzaImpostazioniViewModel()
        {
            Stanza = "null";
            //Carica da API
            CovidMode = false;
            LoadCapienzaCovid();
            LoadCapienzaNormale();
        }


        /// <summary>
        ///     Formatta la visualizzazione della stanza nella Div a destra
        /// </summary>
        /// <returns>Se è stata selezionata una stanza, ritorna la stanza, altrimenti "Seleziona una stanzas"</returns>
        public string GetStanza()
        {
            return Stanza == "null" ? "Seleziona una stanza" : Stanza;
        }

        /// <summary>
        ///     Abilita / Disabilita la modalità Covid
        /// </summary>
        public void ToggleCovidMode()
        {
            CovidMode = !CovidMode;
        }

        /// <summary>
        ///     Carica il Dizionario di CapienzaNormale con valori
        /// </summary>
        private void LoadCapienzaNormale()
        {
            //Carica da API
            CapienzaNormale = new Dictionary<string, int>();
            
            CapienzaNormale.Add("Commerciale", 4);
            CapienzaNormale.Add("Assistenza", 10);
            CapienzaNormale.Add("Sviluppo", 10);
            CapienzaNormale.Add("OpenSpace #1", 10);
            CapienzaNormale.Add("OpenSpace #2", 10);
            CapienzaNormale.Add("Meeting", 12);
            CapienzaNormale.Add("Bansky", 2);
            CapienzaNormale.Add("Contabilità", 4);
        }

        /// <summary>
        ///     Carica il Dizionario di CapienzaCovid con valori
        /// </summary>
        private void LoadCapienzaCovid()
        {
            //Carica da API
            CapienzaCovid = new Dictionary<string, int>();

            CapienzaCovid.Add("Commerciale", 2);
            CapienzaCovid.Add("Assistenza", 5);
            CapienzaCovid.Add("Sviluppo", 5);
            CapienzaCovid.Add("OpenSpace #1", 5);
            CapienzaCovid.Add("OpenSpace #2", 5);
            CapienzaCovid.Add("Meeting", 6);
            CapienzaCovid.Add("Bansky", 1);
            CapienzaCovid.Add("Contabilità", 2);
        }

        /// <summary>
        ///     Ottieni la capienza normale di una stanza 
        /// </summary>
        /// <param name="stanza">Stanza selezionata</param>
        /// <returns>Capienza Normale della stanza selezionata</returns>

        public int GetCapienzaNormale(string stanza)
        {
            if (stanza == null || !CapienzaNormale.ContainsKey(stanza))
                throw new Exception("Stanza non valida");

            return CapienzaNormale[stanza];
        }

        /// <summary>
        ///     Ottieni la capienza covid di una stanza 
        /// </summary>
        /// <param name="stanza">Stanza selezionata</param>
        /// <returns>Capienza Covid della stanza selezionata</returns>
        
        public int GetCapienzaCovid(string stanza)
        {
            if (stanza == null || !CapienzaCovid.ContainsKey(stanza))
                throw new Exception("Stanza non valida");

            return CapienzaCovid[stanza];
        }

        /// <summary>
        ///     Imposta la capienza Normale di una stanza
        /// </summary>
        /// <param name="stanza">Stanza selzionata</param>
        /// <param name="capienza">Capienza selezionata</param>

        public void SetCapienzaNormale(string stanza, int capienza)
        {
            if (capienza <= 0)
                throw new Exception("Capienza non valida");
            if (stanza == null || !CapienzaNormale.ContainsKey(stanza))
                throw new Exception("Stanza non valida");

            CapienzaNormale[stanza] = capienza;
        }

        /// <summary>
        ///     Imposta la capienza Covid di una stanza
        /// </summary>
        /// <param name="stanza">Stanza selzionata</param>
        /// <param name="capienza">Capienza selezionata</param>

        public void SetCapienzaCovid(string stanza, int capienza)
        {
            if (capienza <= 0)
                throw new Exception("Capienza non valida");
            if (stanza == null || !CapienzaCovid.ContainsKey(stanza))
                throw new Exception("Stanza non valida");

            CapienzaCovid[stanza] = capienza;
        }

        /// <summary>
        ///     Funzione utilizzata solo per caricare un numero in caso di fail setCapienza
        /// </summary>
        /// <param name="stanza">Stanza selezionata</param>
        /// <returns>int</returns>

        public int GetCapienzaNormaleDefault(string stanza)
        {
            if (stanza == null || !CapienzaNormale.ContainsKey(stanza))
                return 0;

            return CapienzaNormale[stanza];
        }

        /// <summary>
        ///     Funzione utilizzata solo per caricare un numero in caso di fail setCapienza
        /// </summary>
        /// <param name="stanza">Stanza selezionata</param>
        /// <returns>int</returns>

        public int GetCapienzaCovidDefault(string stanza)
        {
            if (stanza == null || !CapienzaCovid.ContainsKey(stanza))
                return 0;

            return CapienzaCovid[stanza];
        }

    }
}
