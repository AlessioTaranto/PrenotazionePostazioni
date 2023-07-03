using prenotazione_postazioni_libs.Dto;
using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/voti")]
    public class VoteController : ControllerBase
    {
        private VoteService _voteService;
        private readonly ILog _logger = LogManager.GetLogger(typeof(VoteController));

        public VoteController(VoteService voteService)
        {
            this._voteService = voteService;
        }
        /// <summary>
        /// Serve per ottenere l'elenco di votazioni effettuate da un utente verso gli altri
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns>
        /// Restituisce una lista di voti in caso di ricerca con esito positivo
        /// </returns>
        [HttpGet]
        [Route("getUserVotes")]
        public IActionResult GetUserVotes(int idUser)
        {
            try
            {
                _logger.Info("Id utente: " + idUser);
                _logger.Info("Trovando tutti i voti effettuati di un utente...");
                List<Vote> votes = _voteService.GetUserVotes(idUser);
                _logger.Info("Voti dell'utente trovati con successo!");
                return Ok(votes);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Serve per ottenere l'elenco di tutte le votazioni che un utente ha ricevuto
        /// </summary>
        /// <param name="idVictim"></param>
        /// <returns>
        /// Restituisce una lista di voti in caso di ricerca con esito positivo
        /// </returns>
        [Route("getVictimVotes")]
        [HttpGet]
        public IActionResult GetVictimVotes(int idVictim)
        {
            try
            {
                _logger.Info("Id utente: " + idVictim);
                _logger.Info("Trovando tutti i voti che sono stati effettuati su un utente...");
                List<Vote> votes = _voteService.GetVictimVotes(idVictim);
                _logger.Info("Voti trovati con successo!");
                return Ok(votes);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Aggiunge una lista di utenti votati da un altro utente
        /// </summary>
        /// <param name="voteDto"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public IActionResult Add([FromBody] VotoDto voteDto)
        {
            try
            {
                _logger.Info("Voto utente: " + voteDto.IdUtente);
                _logger.Info("Voto utente votato: " + voteDto.IdUtenteVotato);
                _logger.Info("Effettuando un voto su un utente...");
                _voteService.UpdateVote(voteDto);
                _logger.Info("Voto effettuato con successo!");
                return Ok();
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Info("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int idUser, int idVictim)
        {
            try
            {
                _logger.Info("Id utente :" + idUser);
                _logger.Info("Id utente votato: " + idVictim);
                _logger.Info($"Eliminazione voto di {idUser} verso {idVictim}");
                _voteService.DeleteVoto(idUser, idVictim);
                return Ok();
            }

            catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Info("Voto not found: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

    }
}

