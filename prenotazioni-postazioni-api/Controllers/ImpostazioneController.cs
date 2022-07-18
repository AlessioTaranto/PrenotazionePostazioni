using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Repositories;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Exceptions;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/impostazioni")]
    public class ImpostazioneController : ControllerBase
    {
        private ImpostazioneService _impostazioneService = new ImpostazioneService();
        private readonly ILogger<ImpostazioneController> _logger;

        public ImpostazioneController(ILogger<ImpostazioneController> logger)
        {
            _logger = logger;
        }



        /// <summary>
        /// Serve per restituire lo stato dell'impostazione di emergenza
        /// </summary>
        /// <returns>Lo stato di emergenza. True o False</returns>
        [HttpGet]
        [Route("getImpostazioneEmergenza")]
        public IActionResult GetImpostazioneEmergenza()
        {
            try
            {
                return Ok(_impostazioneService.GetImpostazioneEmergenza());
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        /// <summary>
        /// Serve per cambiare lo stato delll'impostazione di emergenza
        /// </summary>
        /// <param name="userValue">
        /// il valore inserito dall'utente per cambiare l'impostazione di emergenza
        /// </param>
        [HttpPost]
        [Route("changeImpostazioneEmergenza")]
        public IActionResult ChangeImpostazioneEmergenza()
        {
            try
            {
                _impostazioneService.ChangeImpostazioniEmergenza();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
