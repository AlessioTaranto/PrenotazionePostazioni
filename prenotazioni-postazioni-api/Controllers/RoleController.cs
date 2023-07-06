
 using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Controllers
{

    [ApiController]
    [Route("/api/role")]
    public class RoleController : ControllerBase
    {
        private RoleService _roleService;
        private readonly ILog _logger = LogManager.GetLogger(typeof(RoleController));

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }




        /// <summary>
        /// Restituisce il ruolo di un utente mediante l'id dell'utente
        /// </summary>
        /// <param name="idUtente">L'id dell'utente per trovare il ruolo associato ad esso</param>
        /// <returns>L'utente trovato con 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getById")]
        public IActionResult GetById(int idRole)
        {
            try
            {
                _logger.Info("Id ruolo: " + idRole);
                _logger.Info("Prelevando un ruolo mediante Ruolo Id...");
                Role ruolo = _roleService.GetById(idRole);
                _logger.Info("Ruolo prelevato con successo!");
                return Ok(ruolo);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Ruolo non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Serve a ottenere il Ruolo di un utente tramite l'id dell'utente
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns>Il ruolo e stato 200 in caso di ricerca effettuata con successo, 404 altrimenti</returns>
        [HttpGet]
        [Route("getByIdUser")]
        public IActionResult GetByIdUser(int idUser)
        {
            try
            {
                _logger.Info("Id utente: " + idUser);
                _logger.Info("Trovando un ruolo mediante l'id dell'utente...");
                Role role = _roleService.GetByIdUser(idUser);
                _logger.Info("Ruolo dell'id utente: " + idUser + " trovato con successo!");
                return Ok(role);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Ruolo non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Aggiorna il ruolo di un utente dall'admin
        /// </summary>
        /// <param name="utenteAdminDto">L'utente che gli verra aggiornato il ruolo</param>
        /// <returns>Ok, Not Authorized altrimenti</returns>

        [HttpPut]
        [Route("update")]
        public IActionResult Update(int idUser,int idAdmin, string futureRole)
        {
            try
            {
                _logger.Info("Id Utente: " + idUser);
                _logger.Info("Id admin: " + idAdmin);
                _logger.Info("Aggiornando il ruolo di un utente...");
                bool ok = _roleService.Update(idUser, idAdmin, futureRole);
                _logger.Info("Controllando se l'autorizzazione e' valida...");
                if (ok)
                {
                    _logger.Info("Autorizzazione riconosciuta!");
                    return Ok();
                }
                else
                {
                    _logger.Error("Autorizzazione NON riconosciuta");
                    return Forbid("Non autorizzato");
                }
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Error("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}