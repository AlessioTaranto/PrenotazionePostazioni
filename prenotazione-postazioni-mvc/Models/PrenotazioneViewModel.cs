using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using System.Xml.Schema;

namespace prenotazione_postazioni_mvc.Models
{
    public class PrenotazioneViewModel
    {
        private UtenteHttpService utenteHttpService;
        private PrenotazioniHttpService prenotazioniHttpService;
        private StanzeHttpService stanzeHttpService;

        // Giorno selezionato
        public DateTime Date { get; set; }
        // Stanza selezionata
        public string Stanza { get; set; }

        // Inizio selezionato
        public DateTime Start { get; set; }
        // Termine selezionato
        public DateTime End { get; set; }

        public List<Utente> Presenti { get; set; }

        public int CollapsedHour { get; set; }
        // Stato Collapse (List)
        public int CollapsedList { get; set; }

        // Costante: Minima ora selezionabile
        public const int HourStart = 7;
        // Costante: Massima ora selezionabile
        public const int HourEnd = 22;

        public PrenotazioneViewModel(DateTime date, string stanza, DateTime start, DateTime end, List<Utente> presenti, StanzeHttpService stanzeHttpService, UtenteHttpService utenteHttpService, PrenotazioniHttpService prenotazioniHttpService)
        {
            this.stanzeHttpService = stanzeHttpService;
            this.utenteHttpService = utenteHttpService;
            this.prenotazioniHttpService = prenotazioniHttpService;
            Date = date;
            Stanza = stanza;
            Start = start;
            End = end;
            Presenti = presenti;
            UpdateListaPersoneOrario();
        }

        public PrenotazioneViewModel(DateTime date, string stanza, UtenteHttpService utenteHttpService, PrenotazioniHttpService prenotazioniHttpService, StanzeHttpService stanzeHttpService)
        {
            this.utenteHttpService = utenteHttpService;
            this.prenotazioniHttpService = prenotazioniHttpService;
            this.stanzeHttpService = stanzeHttpService;
            Date = date;
            Stanza = stanza;
            Start = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            End = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
            Presenti = new List<Utente>();
            UpdateListaPersoneOrario();
        }

        public PrenotazioneViewModel(string stanza, UtenteHttpService utenteHttpService, PrenotazioniHttpService prenotazioniHttpService, StanzeHttpService stanzeHttpService)
        {
            Date = DateTime.Now;
            Stanza = stanza;
            Start = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            End = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
            Presenti = new List<Utente>();
            this.utenteHttpService = utenteHttpService;
            this.prenotazioniHttpService = prenotazioniHttpService;
            this.stanzeHttpService = stanzeHttpService;
            UpdateListaPersoneOrario();
        }

        public PrenotazioneViewModel(StanzeHttpService stanzeHttpService, PrenotazioniHttpService prenotazioniHttpService, UtenteHttpService utenteHttpService)
        {
            this.utenteHttpService = utenteHttpService;
            this.prenotazioniHttpService = prenotazioniHttpService;
            this.stanzeHttpService = stanzeHttpService;

            Stanza = "null";
            Date = DateTime.Now;
            Start = new DateTime(Date.Year, Date.Month, Date.Day, 9, 0, 0);
            End = new DateTime(Date.Year, Date.Month, Date.Day, 18, 0, 0);
            Presenti = new List<Utente>();
            UpdateListaPersoneOrario();
        }

        public async Task<object> UpdateListaPersoneOrario()
        {
            if (Stanza == null)
                return null;
                //return BadRequest("Nessuna stanza selezionata");
            Stanza? stanza = null;
            try
            {
                stanza = await stanzeHttpService.OnGetStanzaByName(Stanza);
            }
            catch (HttpRequestException ex)
            {
                return null;
                //return BadRequest(ex.Message);
            }

            if (stanza == null)
                return null;
                //return BadRequest("Stanza non trovata!");
            List<Prenotazione>? prenotazioni = await prenotazioniHttpService.OnGetAllPrenotazioniByDate(stanza.IdStanza, Start, End);
            if (prenotazioni == null)
                return null;
                //return Ok("Nessuna prenotazione fatta");
            List<Utente> utentiWithDupes = new List<Utente>();
            foreach (var prenotazione in prenotazioni)
            {
                Utente? utente = await utenteHttpService.OnGetUtenteById(prenotazione.IdUtente);
                if (utente != null)
                    utentiWithDupes.Add(utente);
            }
            List<Utente> utentiWithoutDupes = utentiWithDupes.Distinct(new UtenteEqualityComparer()).ToList();
            List<Utente> utenti = new List<Utente>();
            foreach (Utente utente in utentiWithoutDupes)
            {
                Utente? utenteWithoutDupe = await utenteHttpService.OnGetUtenteById(utente.IdUtente);
                if (utenteWithoutDupe != null)
                    utenti.Add(utenteWithoutDupe);
            }
            Presenti = utenti;
            return null;
            //return Ok("Utenti distinti copiati in ViewModel.Presenti!");
        }
        internal class UtenteEqualityComparer : IEqualityComparer<Utente>
        {
            public bool Equals(Utente? x, Utente? y)
            {
                if (x == null || y == null)
                {
                    return false;
                }
                return x.IdUtente == y.IdUtente;
            }

            public int GetHashCode(Utente obj)
            {
                return obj.IdUtente.GetHashCode();
            }
        }
        /// <summary>
        ///     Ottieni il nome della stanza, se nullo restituisci "Seleziona una stanza"
        /// </summary>
        /// <returns>Stanza != null -> Stanza</returns>

        public string GetStanza()
        {
            return this.Stanza == "null" ? "Seleziona una stanza" : this.Stanza;
        }

        /// <summary>
        ///     Adatta l'orario selezionato al formato del datepicker HTML:
        ///     Se l'ora selezionata ha solo un carattere, aggiunge uno 0 davanti.
        /// </summary>
        /// <param name="number">Ora selezionata</param>
        /// <returns>Ora selezionata formattata</returns>
        
        public string FormatHour(int number)
        {
            return number.ToString().Length == 1 ? ("0" + number.ToString()) : number.ToString();
        }

        /// <summary>
        ///     Abilita / Disabilita Collapse Hour
        /// </summary>
        
        public void ToggleCollapseHour()
        {
            _ = CollapsedHour == 0 ? CollapsedHour = 1 : CollapsedHour = 0;
        }

        /// <summary>
        ///     Abilita / Disabilita Collapse List
        /// </summary>

        public void ToggleCollapseList()
        {
            _ = CollapsedList == 0 ? CollapsedList = 1 : CollapsedList = 0;
        }

        /// <summary>
        ///     Cambia il giorno selezionato e valida il nuovo giorno
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <exception cref="Exception">Giorno precedente a quello odierno, Giorno non valido</exception>

        public void ChangeSelectedDay(int year, int month, int day)
        {

            DateTime nowDate = DateTime.Now;

            if ((year < nowDate.Year) || (month < nowDate.Month && year == nowDate.Year) || (day < nowDate.Day && month == nowDate.Month && year == nowDate.Year))
                throw new Exception("Non puoi selezionare una data precedente alla data corrente");

            try
            {
                Date = new DateTime(year, month, day);
                Start = new DateTime(year, month, day, Start.Hour, 0, 0);
                End = new DateTime(year, month, day, End.Hour, 0, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("La data selezionata non è valida");
            } 

        }

        /// <summary>
        ///     Cambia l'orario di inizio, e valida il nuovo orario
        /// </summary>
        /// <param name="hour">Orario selezionato</param>
        /// <exception cref="Exception">Ora precedente minore del minimo, Ora maggiore del massimo, Orario successivo al termine, Orario non valido</exception>

        public void ChangeSelectedStartHour(int hour)
        {
            if (hour < HourStart)
                throw new Exception("Non puoi selezionare un orario prima delle " + HourStart);

            if (hour > HourEnd)
                throw new Exception("Non puoi selezionare un orario dopo delle " + HourEnd);

            if (hour > End.Hour)
                throw new Exception("Non puoi selezionare un orario d'inizio successivo a quello di termine");

            try
            {
                Start = new DateTime(Date.Year, Date.Month, Date.Day, hour, 0, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("L'orario selezionato non è valido");
            }

        }

        /// <summary>
        ///     Cambia l'orario di termine, e valida il nuovo orario
        /// </summary>
        /// <param name="hour">Orario selezionato</param>
        /// <exception cref="Exception">Ora precedente minore del minimo, Ora maggiore del massimo, Orario precedente all'inizio, Orario non valido</exception>


        public void ChangeSelectedEndHour(int hour)
        {
            if (hour < HourStart)
                throw new Exception("Non puoi selezionare un orario prima delle " + HourStart);

            if (hour > HourEnd)
                throw new Exception("Non puoi selezionare un orario dopo delle " + HourEnd);

            if (hour < Start.Hour)
                throw new Exception("Non puoi selezionare un orario di termine precedente a quello d'inizio");

            try
            {
                End = new DateTime(Date.Year, Date.Month, Date.Day, hour, 0, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("L'orario selezionato non è valido");
            }

        }

    }
}
