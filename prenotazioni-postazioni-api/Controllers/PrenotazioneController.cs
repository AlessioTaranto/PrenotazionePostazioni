using Microsoft.AspNetCore.Mvc;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/prenotazioni")]
    public class PrenotazioneController : Controller
    {
        [HttpGet]
        [Route("/get-prenotazione-by-id")]
        /*public Prenotazione getPrenotazioneById(string id)
        {

        }*/

        [HttpGet]
        [Route("/get-all-prenotazioni")]
        /*public List<Prenotazione> GetAllPrenotazioni()
        {

        }*/

        [HttpGet]
        [Route("/get-prenotazioni-by-stanza")]
        /*public List<Prenotazione> GetPrenotazioneByStanza(string idStanza)
        {

        }*/

        [HttpGet]
        [Route("/get-prenotazioni-by-utente")]
        /*public List<Prenotazione> GetPrenotazioneByUtente(string idUtente)
        {

        }*/


        [HttpPost]
        [Route("/add-prenotazione")]
        public bool AddPrenotazione()
        {

        }

    }
}
