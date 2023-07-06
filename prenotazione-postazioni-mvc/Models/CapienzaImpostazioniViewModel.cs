using System.Diagnostics.Contracts;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_mvc.HttpServices;
using prenotazione_postazioni_libs.Models;

namespace prenotazione_postazioni_mvc.Models
{
    public class CapienzaImpostazioniViewModel
    {

        // Stanza selezionata
        public string? Stanza { get; set; }

        // Lista capienza normale di tutte le stanze
        public Dictionary<string, int>? CapienzaNormale { get; set; }

        // Lista capienza covid di tutte le stanze
        public Dictionary<string, int>? CapienzaCovid { get; set; }

        //Http service
        public CapienzaHttpService service { get; set; }

        public CapienzaImpostazioniViewModel(string? stanza, bool covidMode, Dictionary<string, int>? capienzaNormale, Dictionary<string, int>? capienzaCovid, CapienzaHttpService service)
        {
            Stanza = stanza;
            CapienzaNormale = capienzaNormale;
            CapienzaCovid = capienzaCovid;
            this.service = service;
        }

        public CapienzaImpostazioniViewModel(string? stanza, bool covidMode, CapienzaHttpService service)
        {
            Stanza = stanza;
            this.service = service;
            LoadCapienze();
        }

        public CapienzaImpostazioniViewModel(bool covidMode, CapienzaHttpService service)
        {
            Stanza = "null";
            this.service = service;
            LoadCapienze();
        }

        public CapienzaImpostazioniViewModel(CapienzaHttpService service)
        {
            Stanza = "null";
            this.service = service;
            LoadCapienze();
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
        ///     Carica il Dizionario delle Capienze con valori
        /// </summary>
        private async void LoadCapienze()
        {
            CapienzaNormale = new Dictionary<string, int>();
            CapienzaCovid = new Dictionary<string, int>();

            HttpResponseMessage? getAllStance = await service.getAllStanze();
            if (getAllStance == null || getAllStance.StatusCode != HttpStatusCode.OK)
                return;

            List<Room>? Stanze = await getAllStance.Content.ReadFromJsonAsync<List<Room>?>();

            foreach (var Stanza in Stanze)
            {
                CapienzaNormale.Add(Stanza.Name, Stanza.Capacity);
                CapienzaCovid.Add(Stanza.Name, Stanza.CapacityEmergency);
            }
        }

        /// <summary>
        ///     Ottieni la capienza normale di una stanza 
        /// </summary>
        /// <param name="stanza">Stanza selezionata</param>
        /// <returns>Capienza Normale della stanza selezionata</returns>

        public int GetCapienzaNormale(string stanza)
        {

            if (stanza == null)
                return -1;

            if (!CapienzaNormale.ContainsKey(stanza))
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
            if (stanza == null)
                return -1;

            if (stanza == null || !CapienzaCovid.ContainsKey(stanza))
                throw new Exception("Stanza non valida");

            return CapienzaCovid[stanza];
        }

        /// <summary>
        ///     Imposta la capienza Normale di una stanza
        /// </summary>
        /// <param name="stanza">Stanza selzionata</param>
        /// <param name="capienza">Capienza selezionata</param>

        public async void SetCapienzaNormale(string stanza, int capienza)
        {
            if (capienza <= 0)
                throw new Exception("Capienza non valida");
            if (stanza == null || !CapienzaNormale.ContainsKey(stanza))
                throw new Exception("Stanza non valida");

            HttpResponseMessage? setCapienza = await service.setCapienzaStanza(stanza, capienza);
            if (setCapienza == null || setCapienza.StatusCode != HttpStatusCode.OK)
                return;

            LoadCapienze();
        }

        /// <summary>
        ///     Imposta la capienza Covid di una stanza
        /// </summary>
        /// <param name="stanza">Stanza selzionata</param>
        /// <param name="capienza">Capienza selezionata</param>

        public async void SetCapienzaCovid(string stanza, int capienza)
        {
            if (capienza <= 0)
                throw new Exception("Capienza non valida");
            if (stanza == null || !CapienzaCovid.ContainsKey(stanza))
                throw new Exception("Stanza non valida");

            HttpResponseMessage? setCapienza = await service.setCapienzaCovidStanza(stanza, capienza);
            if (setCapienza == null || setCapienza.StatusCode != HttpStatusCode.OK)
                return;

            LoadCapienze();
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
