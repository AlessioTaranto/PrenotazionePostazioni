using prenotazione_postazioni_libs.Dto;
using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/utenti")]
    public class UtenteController : ControllerBase
    {
        private UtenteService _utenteService;
        private readonly ILogger<UtenteController> _logger;

        public UtenteController( ILogger<UtenteController> logger, UtenteService utenteService)
        {
            _utenteService = utenteService;
            _logger = logger;
        }

        [HttpGet]
        [Route("getAllUtenti")]
        public IActionResult GetAllUtenti()
        {
            try
            {
                _logger.LogInformation("Prelevando tutti gli utenti...");
                List<Utente> utenti = _utenteService.getAllUtenti();
                _logger.LogInformation("Prelevato tutti gli utenti con successo!");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce un utente mediante il suo id
        /// </summary>
        /// <param name="id">Id dell'utente da trovare</param>
        /// <returns>L'utente trovato e 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getUtenteById")]
        public IActionResult GetUtenteById(int id)
        {
            try
            {
                _logger.LogInformation("Prelevando un utente mediante il suo id: " + id + "...");
                Utente utente = _utenteService.GetUtenteById(id);
                _logger.LogInformation("Utente trovato con successo!");
                return Ok(utente);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.LogWarning("Utente non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce un utente mediante il suo email
        /// </summary>
        /// <param name="email">L'email dell'utente da trovare</param>
        /// <returns>L'utente trovato e 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getUtenteByEmail")]
        public IActionResult GetUtenteByEmail(string email)
        {
            try
            {
                _logger.LogInformation($"Trovando l'utente mediante il suo email{email}...");
                Utente utente = _utenteService.GetUtenteByEmail(email);
                _logger.LogInformation("Utente trovato mediante il suo email con successo!");
                return Ok(utente);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.LogWarning("Utente non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message); 
            }
        }

        /// <summary>
        /// Aggiunge un utente al database
        /// </summary>
        /// <param name="utenteDto">L'utente da inserire nel database</param>
        /// <returns>httpstatus 200 se salvataggio corretto, httpstatus 400 altrimenti</returns>
        [HttpPost]
        [Route("addNewUtente")]
        public IActionResult AddNewUtente(UtenteDto utenteDto)
        {
            try
            {
                _logger.LogInformation("Salvando un utente nel database...");
                _utenteService.Save(utenteDto);
                _logger.LogInformation("Utente salvato nel database con successo!");
                return Ok();
            }catch(PrenotazionePostazioniApiException ex)
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
