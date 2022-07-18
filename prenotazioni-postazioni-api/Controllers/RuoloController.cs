
 using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Exceptions;

namespace prenotazioni_postazioni_api.Controllers
{

    [ApiController]
    [Route("/api/ruoli")]
    public class RuoloController : ControllerBase
    {
        private RuoloService _ruoloService = new RuoloService();
        private readonly ILogger<RuoloService> _logger;

        public RuoloController(ILogger<RuoloService> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// Restituisce il ruolo di un utente mediante l'id dell'utente
        /// </summary>
        /// <param name="idUtente">L'id dell'utente per trovare il ruolo associato ad esso</param>
        /// <returns>L'utente trovato con 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getRuoloUtente")]
        public IActionResult GetRuoloUtenteById(int idRuolo)
        {
            try
            {
                _logger.LogInformation("Prelevando un ruolo mediante Ruolo Id...");
                Ruolo ruolo = _ruoloService.GetRuoloById(idRuolo);
                _logger.LogInformation("Ruolo prelevato con successo!");
                return Ok(ruolo);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.LogWarning("Ruolo non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Serve a ottenere il Ruolo di un utente tramite l'id dell'utente
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns>Il ruolo e stato 200 in caso di ricerca effettuata con successo, 404 altrimenti</returns>
        [HttpGet]
        [Route("getRuoloByIdUtente")]
        public IActionResult GetRuoloUtenteByIdUtente(int idUtente)
        {
            try
            {
                _logger.LogInformation("Trovando un ruolo mediante l'id dell'utente...");
                Ruolo ruolo = _ruoloService.GetRuoloByIdUtente(idUtente);
                _logger.LogInformation("Ruolo dell'id utente: " + idUtente + " trovato con successo!");
                return Ok(ruolo);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.LogWarning("Ruolo non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Aggiorna il ruolo di un utente dall'admin
        /// </summary>
        /// <param name="utenteAdminDto">L'utente che gli verra aggiornato il ruolo</param>
        /// <returns>Ok, Not Authorized altrimenti</returns>

        [HttpPost]
        [Route("updateRuoloUtenteByUtenteId")]
        public IActionResult UpdateRuoloUtenteByAdminUtenteId([FromRouteAttribute] int idUtente,[FromRouteAttribute] int idAdmin)
        {
            try
            {
                _logger.LogInformation("Aggiornando il ruolo di un utente...");
                bool ok = _ruoloService.UpdateRuoloUtenteByAdminUtenteId(idUtente, idAdmin);
                _logger.LogInformation("Controllando se l'autorizzazione e' valida...");
                if (ok)
                {
                    _logger.LogInformation("Autorizzazione riconosciuta!");
                    return Ok();
                }
                else
                {
                    _logger.LogError("Autorizzazione NON riconosciuta");
                    return Forbid("Non autorizzato");
                }
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.LogError("Bad request: " + ex.Message);
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