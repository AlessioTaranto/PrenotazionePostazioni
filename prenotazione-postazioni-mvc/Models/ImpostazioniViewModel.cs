using prenotazione_postazioni_mvc.HttpServices;
using System.Net;

namespace prenotazione_postazioni_mvc.Models
{
    public class ImpostazioniViewModel
    {

        // Modello del Tab 0 (Covid)
        public CapienzaImpostazioniViewModel CapienzaViewModel { get; set; }

        // Modello del Tab 2 (Presenze)
        public PresenzeImpostazioniViewModel PresenzeViewModel { get; set; }

        // Stato del Tab
        public int StateTab { get; set; } = 0;

        //Festività selezionata
        public DateTime FestaSelezionata { get; set; }

        //Stringa JSON con lista delle feste da leggere in javascript
        public string FesteJSON { get; set; } = "{}";

        // Http Festa service 
        public FestaHttpService _festaHttpService { get; set; }

        public ImpostazioniViewModel(CapienzaImpostazioniViewModel capienzaViewModel, PresenzeImpostazioniViewModel presenzeViewModel, FestaHttpService festaHttpService)
        {
            CapienzaViewModel = capienzaViewModel;
            PresenzeViewModel = presenzeViewModel;
            _festaHttpService = festaHttpService;
        }

        public string GetFestaSelezionata()
        {
            if (FestaSelezionata.Date.Year == 1)
                return "Seleziona un giorno";

            return "Giorno selezionato: " + FestaSelezionata.Date.Day + "/" + FestaSelezionata.Date.Month + "/" + FestaSelezionata.Date.Year;
        }

        public void SelectFesta(int year, int month, int day)
        {
            try
            {
                FestaSelezionata = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Giorno non valido");
            }
        }

        public async void AddFesta(int year, int month, int day, string description)
        {
            DateTime date;

            try
            {
                date = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Giorno non valido");
            }

            HttpResponseMessage addFesta = await _festaHttpService.AddFesta(date, description);
            if (addFesta == null || addFesta.StatusCode != HttpStatusCode.OK)
                return;
        }

        public async Task RemoveFesta(int year, int month, int day)
        {
            DateTime date;

            try
            {
                date = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Giorno non valido");
            }

            HttpResponseMessage removeFesta = await _festaHttpService.RemoveFesta(year, month, day);
            if (removeFesta == null || removeFesta.StatusCode != HttpStatusCode.OK)
                return;
        }

        public async Task ReloadFeste()
        {
            HttpResponseMessage msg = await _festaHttpService.getAllFeste();
            if (msg == null || msg.StatusCode != HttpStatusCode.OK)
                return;

            Task<String> ctxString = msg.Content.ReadAsStringAsync();
            ctxString.Wait();
            FesteJSON = ctxString.Result;
        }


    }
}
