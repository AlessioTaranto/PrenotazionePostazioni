
﻿using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_libs.Dto;

namespace prenotazioni_postazioni_api.Controllers
{

    [ApiController]
    [Route("/api/ruoli")]
    public class RuoloController : ControllerBase
    {
        private RuoloService ruoloService = new RuoloService();
        /// <summary>
        /// Restituisce il ruolo di un utente mediante l'id dell'utente
        /// </summary>
        /// <param name="idUtente">L'id dell'utente per trovare il ruolo associato ad esso</param>
        /// <returns>L'utente trovato con 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getRuoloUtente")]
        public IActionResult GetRuoloUtenteByUtenteId(int idUtente)
        {
            Ruolo ruolo = ruoloService.GetRuoloByUtenteId(idUtente);
            if (ruolo == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(ruolo);
            }
        }


        //da aggiornare la cors policy, oppure implementare un sistema di accesso

        /// <summary>
        /// Aggiorna il ruolo di un utente dall'admin
        /// </summary>
        /// <param name="utenteAdminDto">L'utente che gli verra aggiornato il ruolo</param>
        /// <returns>Ok, Not Authorized altrimenti</returns>

        [HttpPost]
        [Route("updateRuoloUtenteByUtenteId")]
        public IActionResult UpdateRuoloUtenteByAdminUtenteId([FromBody] UtenteDto utenteDto)
        {
            bool ok = ruoloService.UpdateRuoloUtenteByAdminUtenteId(utenteDto);
            if (ok)
            {
                return Ok();
            }
            else
            {
                return Content("Unauthorized");
            }
        }
    }
}
