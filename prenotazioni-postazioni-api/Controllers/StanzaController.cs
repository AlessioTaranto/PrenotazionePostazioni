
 using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Exceptions;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/stanze")]
    public class StanzaController : ControllerBase
    {
        private StanzaService _stanzaService = new StanzaService();

        /// <summary>
        /// Restituisce tutte le stanze
        /// </summary>
        /// <returns>Lista di tutte le stanze</returns>
        [HttpGet]
        [Route("getAllStanze")]
        public IActionResult GetAllStanze()
        {
            return Ok(_stanzaService.GetAllStanze());
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
                Stanza stanza = _stanzaService.GetStanzaById(id);
                return stanza;
            }catch(PrenotazionePostazioniApiException ex)
            {
                return NotFound();
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
                Stanza stanza = _stanzaService.GetStanzaByName(stanzaName);
                return stanza;
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                return NotFound();
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
                _stanzaService.Save(stanzaDto);
                return Ok()
            }catch(PrenotazionePostazioniApiException ex)
            {
                return BadRequest();
            }
        }
    }
}
