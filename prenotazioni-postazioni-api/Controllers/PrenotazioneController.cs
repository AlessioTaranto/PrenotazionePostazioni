
using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/prenotazioni")]
    public class PrenotazioneController : ControllerBase
    {
        private PrenotazioneService _prenotazioneService = new PrenotazioneService();
        
        /// <summary>
        /// Restituisce la Prenotazione trovata mediante il suo ID
        /// </summary>
        /// <param name="idPrenotazione">Id della Prenotazione</param>
        /// <returns>Prenotazione e status 200, status 404 altrimenti</returns>
        [HttpGet]
        [Route("/getPrenotazioneById")]
        public IActionResult GetPrenotazioneById(int idPrenotazione)
        {
            Prenotazione prenotazione = _prenotazioneService.GetPrenotazioneById(idPrenotazione);
            if(prenotazione == null)
            {
                return NotFound();
            }
            return Ok(prenotazione);
        }

        /// <summary>
        /// Restituisce tutte le prenotazioni presenti nel Database
        /// </summary>
        /// <returns>Lista di Prenotazioni e status 200</returns>
        [HttpGet]
        [Route("/getAllPrenotazioni")]
        public IActionResult GetAllPrenotazioni()
        {
            return Ok(_prenotazioneService.GetAllPrenotazioni());
        }

        /// <summary>
        /// Restituisce la Prenotazione associata alla sua stanza.
        /// </summary>
        /// <param name="idStanza">L'Id della stanza associata alla Prenotazione</param>
        /// <returns>Prenotazione e status 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("/getPrenotazioniByStanza")]
        public IActionResult GetPrenotazioneByStanza(string idStanza)
        {
            Prenotazione prenotazione = _prenotazioneService.GetPrenotazioneByStanza(idStanza);
            if(prenotazione == null)
            {
                return NotFound();
            }
            return Ok(prenotazione);
        }
        
        /// <summary>
        /// Restituisce una Prenotazione dall'Id utente associato
        /// </summary>
        /// <param name="idUtente">L'id utente associata alla Prenotazione</param>
        /// <returns>Prenotazione e status 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("/getPrenotazioniByUtente")]
        public IActionResult GetPrenotazioneByUtente(string idUtente)
        {
            Prenotazione prenotazione = _prenotazioneService.GetPrenotazioneByUtente(idUtente);
            if (prenotazione == null)
            {
                return NotFound();
            }
            return Ok(prenotazione);
        }

        /// <summary>
        /// Salva una prenotazione nel database
        /// </summary>
        /// <param name="prenotazioneDto">L'oggetto dto da mappare e poi salvare</param>
        /// <returns>status 200</returns>
        // [HttpPost]
        // [Route("/add-prenotazione")]
        // public IActionResult AddPrenotazione([FromBody] PrenotazioneDto prenotazioneDto)
        // {
        //     return Ok(_prenotazioneService.Save(prenotazioneDto));
        // }

    }
}
