using Microsoft.AspNetCore.Mvc;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/voti")]
    public class VotoController : ControllerBase
    {
        /// <summary>
        /// Serve per ottenere l'elenco di votazioni effettuate da un utente verso gli altri
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns>
        /// Restituisce una lista di voti in caso di ricerca con esito positivo
        /// </returns>
        [Route("getVotiFatti")]
        public IActionResult GetVotiFromUtente(int idUtente)
        {

        }

        /// <summary>
        /// Serve per ottenere l'elenco di tutte le votazioni che un utente ha ricevuto
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns>
        /// Restituisce una lista di voti in caso di ricerca con esito positivo
        /// </returns>
        [Route("getVotiOfUtente")]
        public IActionResult GetVotiToUtente(int idUtente)
        {

        }



    }
}
