using prenotazione_postazioni_libs.Dto;
using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using prenotazioni_postazioni_api.Utilities;
using log4net;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/voti")]
    public class VotoController : ControllerBase
    {
        private VotoService _votoService;
        private readonly ILog logger = LogManager.GetLogger(typeof(VotoController));

        public VotoController(VotoService votoService)
        {
            this._votoService = votoService;
        }
        /// <summary>
        /// Serve per ottenere l'elenco di votazioni effettuate da un utente verso gli altri
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns>
        /// Restituisce una lista di voti in caso di ricerca con esito positivo
        /// </returns>
        [HttpGet]
        [Route("getVotiFromUtente")]
        public IActionResult GetVotiFromUtente(int idUtente)
        {
            try
            {
                _logger.LogInformation("Trovando tutti i voti effettuati di un utente...");
                List<Voto> voti = _votoService.GetVotiFromUtente(idUtente);
                _logger.LogInformation("Voti dell'utente trovati con successo!");
                return Ok(voti);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Serve per ottenere l'elenco di tutte le votazioni che un utente ha ricevuto
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns>
        /// Restituisce una lista di voti in caso di ricerca con esito positivo
        /// </returns>
        [Route("getVotiToUtente")]
        [HttpGet]
        public IActionResult GetVotiToUtente(int idUtente)
        {
            try
            {
                _logger.LogInformation("Trovando tutti i voti che sono stati effettuati su un utente...");
                List<Voto> voti = _votoService.GetVotiToUtente(idUtente);
                _logger.LogInformation("Voti trovati con successo!");
                return Ok(voti);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Aggiunge una lista di utenti votati da un altro utente
        /// </summary>
        /// <param name="votoDto"></param>
        /// <returns></returns>
        [Route("makeVotoToUtente")]
        [HttpPost]
        public IActionResult MakeVotoToUtente([FromBody] VotoDto votoDto)
        {
            try
            {
                _logger.LogInformation("Effettuando un voto su un utente...");
                _votoService.MakeVotoToUtente(votoDto);
                _logger.LogInformation("Voto effettuato con successo!");
                return Ok();
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                _logger.LogInformation("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}

