using System.Net;
using Newtonsoft.Json;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;

namespace prenotazione_postazioni_mvc.Models
{
    public class FestivitaImpostazioniViewModel
    {

        // Giorno selezionato
        public DateTime GiornoSelezionato { get; set; }
        // Lista di tutte le festività
        public List<DateTime> Festivita { get; set; }
        // Http service
        public FestaHttpService service { get; set; }

        public FestivitaImpostazioniViewModel(DateTime giornoSelezionato, List<DateTime> festivita, FestaHttpService service)
        {
            GiornoSelezionato = giornoSelezionato;
            Festivita = festivita;
            this.service = service;
            LoadFeste();
        }

        public FestivitaImpostazioniViewModel(List<DateTime> festivita, FestaHttpService service)
        {
            Festivita = festivita;
            this.service = service;
            LoadFeste();
        }

        public FestivitaImpostazioniViewModel(FestaHttpService service)
        {
            Festivita = new List<DateTime>();
            this.service = service;
            LoadFeste();
        }

        public async void LoadFeste()
        {
            Festivita.Clear();

            HttpResponseMessage getAllFeste = await service.getAllFeste();
            if (getAllFeste == null || getAllFeste.StatusCode != HttpStatusCode.OK)
                return;

            List<Festa>? festeList = await getAllFeste.Content.ReadFromJsonAsync<List<Festa>?>();

            foreach (var festa in festeList)
            {
                Festivita.Add(festa.Giorno);
            }
        }

        /// <summary>
        ///     Aggiungi una festività alla lista delle Festività
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <exception cref="Exception">Giorno non valido, Festività già inserita</exception>

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

            if (Festivita.Contains(date))
                throw new Exception("Festività già inserita");

            HttpResponseMessage addFesta = await service.AddFesta(date, description);
            if (addFesta == null || addFesta.StatusCode != HttpStatusCode.OK)
                return;

            LoadFeste();
        }

        /// <summary>
        ///     Rimuovi una festività dalla lista delle Festività
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <exception cref="Exception">Giorno non valido, Festività non inserita</exception>

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

            if (!Festivita.Contains(date))
                throw new Exception("Festività non presente");

            HttpResponseMessage removeFesta = await service.RemoveFesta(year, month, day);
            if (removeFesta == null || removeFesta.StatusCode != HttpStatusCode.OK)
                return;

            LoadFeste();
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

        /// <summary>
        ///     Seleziona un giorno nel Calendar del tab "Festività"
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <exception cref="Exception">Giorno non valido</exception>

        public void SelectFesta(int year, int month, int day)
        {
            try
            {
                GiornoSelezionato = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Giorno non valido");
            }
        }

        public string SerializeFeste()
        {
            return JsonConvert.SerializeObject(Festivita);
        }
    }
}
