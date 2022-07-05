using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/utenti")]
    public class UtenteCotroller : ControllerBase
    {
        private UtenteService utenteService = new UtenteService();

        /// <summary>
        /// Restituisce un utente mediante il suo id
        /// </summary>
        /// <param name="id">Id dell'utente da trovare</param>
        /// <returns>L'utente trovato e 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getUtenteById")]
        public IActionResult GetUtenteById(int id)
        {
            Utente utente = utenteService.GetUtenteById(id);
            if (utente == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(utente);
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
            Utente utente = utenteService.GetUtenteByEmail(email);
            if(utente == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(utente);
            }
        }

        /// <summary>
        /// Aggiunge un utente al database
        /// </summary>
        /// <param name="utenteDto">L'utente da inserire nel database</param>
        /// <returns>httpstatus 200</returns>

        [HttpPost]
        [Route("addNewUtente")]
        public IActionResult AddNewUtente(UtenteDto utenteDto)
        {
            return Ok(utenteService.Save(utenteDto));
        }
        
    }
}
