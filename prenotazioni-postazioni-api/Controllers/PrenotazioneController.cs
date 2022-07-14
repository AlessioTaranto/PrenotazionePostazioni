
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
        private PrenotazioneService _prenotazioneService = new PrenotazioneService();
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
                Prenotazione prenotazione = _prenotazioneService.GetPrenotazioneById(idPrenotazione);
                return Ok(prenotazione);
            }catch(PrenotazionePostazioniApiException ex)
            {
                return NotFound(ex.Message);
            }catch (Exception ex)
            {
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
                return Ok(_prenotazioneService.GetAllPrenotazioni());
            }
            catch (Exception ex)
            {
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
                List<Prenotazione>   prenotazione = _prenotazioneService.GetPrenotazioniByStanza(idStanza);
                return Ok(prenotazione);
            }catch(PrenotazionePostazioniApiException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
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
                List<Prenotazione> prenotazioni = _prenotazioneService.GetPrenotazioniByUtente(idUtente);
                return Ok(prenotazioni);
            }catch(PrenotazionePostazioniApiException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
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
                List<Prenotazione> prenotazioni = _prenotazioneService.GetAllPrenotazioniByIdStanzaAndDate(idStanza, startDate, endDate);
                return Ok(prenotazioni);
            }catch(PrenotazionePostazioniApiException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
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
                _prenotazioneService.Save(prenotazioneDto);
                return Ok();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
