using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using prenotazione_postazioni_mvc.Models;
using System.Diagnostics.CodeAnalysis;

namespace prenotazione_postazioni_mvc.Controllers
{
    public class UtentiController : Controller
    {
        public PrenotazioneViewModel? ViewModel { get; set; }

        private PrenotazioniHttpService prenotazioniHttpService;
        private UtenteHttpService utenteHttpService;

        public UtentiController(PrenotazioniHttpService prenotazioniHttpService, UtenteHttpService utenteHttpService)
        {
            this.prenotazioniHttpService = prenotazioniHttpService;
            this.utenteHttpService = utenteHttpService;
        }

        public IActionResult Index()
        {
            return View(ViewModel);
        }



        [HttpPost]
        [ActionName("UpdateStartDate")]
        public IActionResult UpdateStartDate(DateTime startDate)
        {
            if (ViewModel != null)
                ViewModel.Start = startDate;

            return Ok("Inizio ora aggiornato");
        }



        [HttpPost]
        [ActionName("UpdateEndDate")]
        public IActionResult UpdateEndDate(DateTime endDate)
        {
            if (ViewModel != null)
                ViewModel.End = endDate;
            return Ok("End date aggiornato");
        }

        [HttpPost]
        [ActionName("UpdateListaPersoneOrario")]
        public async Task<IActionResult> UpdateListaPersoneOrario(int idStanza)
        {
            if (ViewModel.End == null || ViewModel.Start == null)
                return BadRequest("Errore in inizio data oppure fine data!");
            List<Prenotazione>? prenotazioni = await prenotazioniHttpService.OnGetAllPrenotazioniByDate(idStanza, ViewModel.Start, ViewModel.End);
            if (prenotazioni == null)
                return Ok("Nessuna prenotazione fatta");
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
            ViewModel.Presenti = utenti;
            return Ok("Utenti distinti copiati in ViewModel.Presenti!");
        }

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
}
