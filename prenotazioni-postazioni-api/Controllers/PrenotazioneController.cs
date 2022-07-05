using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/prenotazioni")]
    public class PrenotazioneController : ControllerBase
    {
        private PrenotazioneService prenotazioneService = new PrenotazioneService();

        /// <summary>
        /// Restituisce la Prenotazione trovata mediante il suo ID
        /// </summary>
        /// <param name="idPrenotazione">Id della Prenotazione</param>
        /// <returns>Prenotazione e status 200, status 404 altrimenti</returns>
        [HttpGet]
        [Route("/get-prenotazione-by-id")]
        public IActionResult GetPrenotazioneById(int idPrenotazione)
        {
            Prenotazione prenotazione = prenotazioneService.GetPrenotazioneById(idPrenotazione);
            if(prenotazione == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prenotazione);
            }
        }

        /// <summary>
        /// Restituisce tutte le prenotazioni presenti nel Database
        /// </summary>
        /// <returns>Lista di Prenotazioni e status 200</returns>
        [HttpGet]
        [Route("/get-all-prenotazioni")]
        public IActionResult GetAllPrenotazioni()
        {
            return Ok(prenotazioneService.GetAllPrenotazioni());
        }

        /// <summary>
        /// Restituisce la Prenotazione associata alla sua stanza.
        /// </summary>
        /// <param name="idStanza">L'Id della stanza associata alla Prenotazione</param>
        /// <returns>Prenotazione e status 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("/get-prenotazioni-by-stanza")]
        public IActionResult GetPrenotazioneByStanza(string idStanza)
        {
            Prenotazione prenotazione = prenotazioneService.GetPrenotazioneByStanza(idStanza);
            if(prenotazione == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prenotazione);
            }
        }

        /// <summary>
        /// Restituisce una Prenotazione dall'Id utente associato
        /// </summary>
        /// <param name="idUtente">L'id utente associata alla Prenotazione</param>
        /// <returns>Prenotazione e status 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("/get-prenotazioni-by-utente")]
        public IActionResult GetPrenotazioneByUtente(string idUtente)
        {
            Prenotazione prenotazione = prenotazioneService.GetPrenotazioneByUtente(idUtente);
            if (prenotazione == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prenotazione);
            }
        }

        /// <summary>
        /// Salva una prenotazione nel database
        /// </summary>
        /// <param name="prenotazioneDto">L'oggetto dto da mappare e poi salvare</param>
        /// <returns>status 200</returns>
        [HttpPost]
        [Route("/add-prenotazione")]
        public IActionResult AddPrenotazione([FromBody] PrenotazioneDto prenotazioneDto)
        {
            return Ok(prenotazioneService.Save(prenotazioneDto));
        }

    }
}
