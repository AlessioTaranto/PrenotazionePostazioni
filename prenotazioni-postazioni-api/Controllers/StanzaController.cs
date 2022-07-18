
 using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Exceptions;
using Microsoft.AspNetCore.Cors;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/stanze")]
    public class StanzaController : ControllerBase
    {
        private StanzaService _stanzaService = new StanzaService();
        private ILogger<StanzaController> _logger;

        public StanzaController(ILogger<StanzaController> logger)
        {
            this._logger = logger;
        }
        /// <summary>
        /// Restituisce tutte le stanze
        /// </summary>
        /// <returns>Lista di tutte le stanze</returns>
        [HttpGet]
        [Route("getAllStanze")]
        public IActionResult GetAllStanze()
        {
            try
            {
                _logger.LogInformation("Trovando tutte le stanze...");
                List<Stanza> stanze = _stanzaService.GetAllStanze();
                _logger.LogInformation("Stanze trovate con successo!");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        
        /// <summary>
        /// Restituisce la stanza mediante l'id associato
        /// </summary>
        /// <param name="id">L'id della stanza</param>
        /// <returns>La stanza trovata con 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getStanzeById")]
        public IActionResult GetStanzaById(int id)
        {
            try
            {
                _logger.LogInformation("Trovando la stanza mediante il suo id: " + id + "...");
                Stanza stanza = _stanzaService.GetStanzaById(id);
                _logger.LogInformation("Stanza trovata con successo!");
                return Ok(stanza);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.LogWarning("Stanza non trovata: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return BadRequest();
            }
            
        }

        /// <summary>
        /// Restituisce la stanza mediante il suo nome
        /// </summary>
        /// <param name="stanzaName">Il nome della stanza da trovare</param>
        /// <returns>La stanza trovata e 200, 404 altrimenti </returns>
        [HttpGet]
        [Route("getStanzaByName")]
        public IActionResult GetStanzaByName(string stanzaName)
        {
            try
            {
                _logger.LogInformation("Trovando la stanza mediante il suo nome: " + stanzaName + "...");
                Stanza stanza = _stanzaService.GetStanzaByName(stanzaName);
                _logger.LogInformation("Stanza trovata con successo!");
                return Ok(stanza);
            }
            catch (PrenotazionePostazioniApiException ex)
            {

                _logger.LogWarning("stanza non trovata: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Aggiunge una nuova stanza al database
        /// </summary>
        /// <param name="stanzaDto">L'oggetto stanza da aggiungere al database</param>
        /// <returns>httpstatus 200</returns>
        [HttpPost]
        [Route("addStanza")]
        public IActionResult AddNewStanza(StanzaDto stanzaDto)
        {
            try
            {
                _logger.LogInformation("Salvando una stanzaDto nel database...");
                _stanzaService.Save(stanzaDto);
                _logger.LogInformation("StanzaDto salvato nel database con successo!");
                return Ok();
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.LogWarning("bad request: " + ex.Message);
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
