
﻿using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/stanze")]
    public class StanzaController : ControllerBase
    {
        private StanzaService stanzaService = new StanzaService();

        /// <summary>
        /// Restituisce tutte le stanze
        /// </summary>
        /// <returns>Lista di tutte le stanze</returns>
        [HttpGet]
        [Route("getAllStanze")]
        public IActionResult GetAllStanze()
        {
            return Ok(stanzaService.GetAllStanze());
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
            Stanza stanza = stanzaService.GetStanzaByid(id);
            if (stanza == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(stanza);
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
            Stanza stanza = stanzaService.GetStanzaByName(stanzaName);
            if(stanza == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(stanza);
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
            return Ok(stanzaService.Save(stanzaDto));
        }
    }
}
