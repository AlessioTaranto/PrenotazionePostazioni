using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Repositories;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/settings")]
    public class SettingsController : ControllerBase
    {
        private SettingsService _settingsService;
        private readonly ILog _logger = LogManager.GetLogger(typeof(SettingsController));

        public SettingsController(SettingsService settingsService)
        {
            _settingsService = settingsService;
        }



        /// <summary>
        /// Serve per restituire lo stato dell'impostazione di emergenza
        /// </summary>
        /// <returns>Lo stato di emergenza. True o False</returns>
        [HttpGet]
        [Route("get")]
        public IActionResult Get()
        {
            try
            {
                _logger.Info("Prelevando l'impostazione di emergenza...");
                bool modEmergency = _settingsService.Get();
                _logger.Info("Trovato impostazione di emergenza con successo!");
                return Ok(modEmergency);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Impostazione non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.Fatal("Errore Interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
            
        }

        /// <summary>
        /// Serve per cambiare lo stato delll'impostazione di emergenza
        /// </summary>
        /// <param name="userValue">
        /// il valore inserito dall'utente per cambiare l'impostazione di emergenza
        /// </param>
        [HttpPut]
        [Route("update")]
        public IActionResult Update()
        {
            try
            {
                _logger.Info("Cambiando l'impostazione di emergenza...");
                _settingsService.Update();
                _logger.Info("Impostazione di emergenza cambiato con successo");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore Interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
