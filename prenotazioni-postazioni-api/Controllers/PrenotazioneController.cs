
using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("api/prenotazioni")]
    public class PrenotazioneController : ControllerBase
    {
        private PrenotazioneService _prenotazioneService;
        private readonly ILogger<PrenotazioneController> _logger;

        public PrenotazioneController(ILogger<PrenotazioneController> logger, PrenotazioneService prenotazioneService)
        {
            _prenotazioneService = prenotazioneService;
            _logger = logger;
        }
        /// <summary>
        /// Restituisce la Prenotazione trovata mediante il suo ID
        /// </summary>
        /// <param name="idPrenotazione">Id della Prenotazione</param>
        /// <returns>Prenotazione e status 200, status 404 altrimenti</returns>
        [HttpGet]
        [Route("getPrenotazioneById")]
        public IActionResult GetPrenotazioneById(int idPrenotazione)
        {
            try
            {
                _logger.LogInformation("Trovando una prenotazione mediante l'id...");
                Prenotazione prenotazione = _prenotazioneService.GetPrenotazioneById(idPrenotazione);
                _logger.LogInformation("Trovato una prenotazione con id: " + prenotazione.IdPrenotazioni + " con successo");
                return Ok(prenotazione);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.LogWarning("Prenotazione non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }catch (Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce tutte le prenotazioni presenti nel Database
        /// </summary>
        /// <returns>Lista di Prenotazioni e status 200</returns>
        [HttpGet]
        [Route("getAllPrenotazioni")]
        public IActionResult GetAllPrenotazioni()
        {
            try
            {
                _logger.LogInformation("Trovando tutte le prenotazioni...");
                List<Prenotazione> prenotazioni = _prenotazioneService.GetAllPrenotazioni();
                _logger.LogInformation("Prenotazioni trovate con successo!");
                return Ok(prenotazioni);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce la Prenotazione associata alla sua stanza.
        /// </summary>
        /// <param name="idStanza">L'Id della stanza associata alla Prenotazione</param>
        /// <returns>Lista di Prenotazione e status 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getPrenotazioniByStanza")]
        public IActionResult GetPrenotazioniByStanza(int idStanza)
        {
            try
            {
                _logger.LogInformation("Trovando tutte le prenotazioni di una stanza...");
                List<Prenotazione> prenotazioni = _prenotazioneService.GetPrenotazioniByStanza(idStanza);
                _logger.LogInformation("Prenotazioni della stanza ID: " + idStanza + " trovate!");
                return Ok(prenotazioni);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.LogWarning("Non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }

        }
        
        /// <summary>
        /// Restituisce tutte le Prenotazioni dall'Id utente associato
        /// </summary>
        /// <param name="idUtente">L'id utente associata alla Prenotazione</param>
        /// <returns>Lista di Prenotazione e status 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getPrenotazioniByUtente")]
        public IActionResult GetPrenotazioneByUtente(int idUtente)
        {
            try
            {
                _logger.LogInformation("Trovando tutte le prenotazioni di un utente");
                List<Prenotazione> prenotazioni = _prenotazioneService.GetPrenotazioniByUtente(idUtente);
                _logger.LogInformation("Prenotazioni dell'id utente: " + idUtente + " trovate con successo!");
                return Ok(prenotazioni);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.LogWarning("Non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce tutte le Prenotazioni effettuate in una Stanza
        /// </summary>
        /// <param name="idStanza"></param>
        /// <param name="date"></param>
        /// <returns>Lista di Prenotazioni e status 200, altrimenti 404</returns>
        [HttpGet]
        [Route("getPrenotazioniByDate")]
        public IActionResult GetPrenotazioniByDate(int idStanza, DateTime startDate, DateTime endDate)
        {
            try
            {
                _logger.LogInformation("Date inserite: ");
                _logger.LogInformation("Id Stanza: " + idStanza);
                _logger.LogInformation("StartDate: " + startDate.ToString());
                _logger.LogInformation("EndDate: " + endDate.ToString());
                _logger.LogInformation("Trovando tutte le prenotazioni di una data...");
                List<Prenotazione> prenotazioni = _prenotazioneService.GetAllPrenotazioniByIdStanzaAndDate(idStanza, startDate, endDate);
                _logger.LogInformation("Prenotazioni della stanza trovate con successo");
                return Ok(prenotazioni);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.LogWarning("Non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Salva una prenotazione nel database
        /// </summary>
        /// <param name="prenotazioneDto">L'oggetto dto da mappare e poi salvare</param>
        /// <returns>status 200</returns>
        [HttpPost]
        [Route("addPrenotazione")]
        public IActionResult AddPrenotazione([FromBody] PrenotazioneDto prenotazioneDto)
        {
            try
            {
                _logger.LogInformation("Aggiungendo una prenotazioneDto nel database...");
                _prenotazioneService.Save(prenotazioneDto);
                _logger.LogInformation("PrenotazioneDto aggiunto con successo!");
                return Ok();
            }
            catch(ArgumentException ex)
            {
                _logger.LogError("Errore insertimento del parametro: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                _logger.LogError("Errore: " + ex.Message);
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
