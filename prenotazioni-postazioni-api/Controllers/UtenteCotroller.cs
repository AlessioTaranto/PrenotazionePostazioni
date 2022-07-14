using prenotazione_postazioni_libs.Dto;
using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/utenti")]
    public class UtenteCotroller : ControllerBase
    {
        private UtenteService _utenteService = new UtenteService();

        [HttpGet]
        [Route("getAllUtenti")]
        public IActionResult GetAllUtenti()
        {
            try
            {
                return Ok(_utenteService.getAllUtenti);
            }
            catch (Exception ex)
            {
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
                Utente utente = _utenteService.GetUtenteById(id);
                return Ok(utente);
            }catch(PrenotazionePostazioniApiException ex)
            {
                return NotFound("Utente non trovato");
            }
            catch (Exception ex)
            {
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
                Utente utente = _utenteService.GetUtenteByEmail(email);
                return Ok(utente);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                return NotFound("Utente non trovato");
            }
            catch (Exception ex) 
            { 
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
                _utenteService.Save(utenteDto);
                return Ok();
            }catch(PrenotazionePostazioniApiException ex)
            {
                return BadRequest("Impossibile aggiungere nuovo utente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
    }
}
