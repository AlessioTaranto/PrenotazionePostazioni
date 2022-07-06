using prenotazione_postazioni_libs.Dto;
using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/voti")]
    public class VotoController : ControllerBase
    {
        private VotoService votoService = new VotoService();


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
            return Ok(votoService.GetVotiFromUtente(idUtente));
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
            return Ok(votoService.GetVotiToUtente(idUtente));
        }


        /// <summary>
        /// Aggiunge una lista di utenti votati da un altro utente
        /// </summary>
        /// <param name="utenteTo">L'utente che ha votato</param>
        /// <param name="utenteFrom">L'utente che ha subito la votazione di utenteTo</param>
        /// <returns></returns>
        [Route("makeVotoToUtente")]
        [HttpPost]
        public IActionResult makeVotoToUtente([FromBody] UtenteDto utenteTo, List<UtenteDto> utenteFrom)
        {
            return Ok(votoService.MakeVotoToUtente(utenteTo, utenteFrom));
        }
    }
}

