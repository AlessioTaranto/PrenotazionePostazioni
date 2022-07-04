using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/prenotazioni")]
    public class PrenotazioneController : Controller
    {
        private PrenotazioneService prenotazioneService = new PrenotazioneService();

        [HttpGet]
        [Route("/get-prenotazione-by-id")]
        public IActionResult GetPrenotazioneById(int id)
        {
            Prenotazione prenotazione = prenotazioneService.
        }

        [HttpGet]
        [Route("/get-all-prenotazioni")]
        public IActionResult GetAllPrenotazioni()
        {

        }

        [HttpGet]
        [Route("/get-prenotazioni-by-stanza")]
        public IActionResult GetPrenotazioneByStanza(string idStanza)
        {

        }

        [HttpGet]
        [Route("/get-prenotazioni-by-utente")]
        public IActionResult GetPrenotazioneByUtente(string idUtente)
        {

        }


        [HttpPost]
        [Route("/add-prenotazione")]
        public IActionResult AddPrenotazione()
        {

        }

    }
}
