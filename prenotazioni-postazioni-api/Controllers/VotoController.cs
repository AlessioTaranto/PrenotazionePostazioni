using prenotazione_postazioni_libs.Dto;
using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/voti")]
    public class VotoController : ControllerBase
    {
        private VotoService _votoService = new VotoService();
        
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
                return Ok(_votoService.GetVotiFromUtente(idUtente));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
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
                return Ok(_votoService.GetVotiToUtente(idUtente));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
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
                _votoService.MakeVotoToUtente(votoDto);
                return Ok();
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                return BadRequest("Impossibile effettuare il voto");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}

