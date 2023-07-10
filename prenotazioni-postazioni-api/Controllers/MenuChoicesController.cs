using prenotazione_postazioni_libs.Dto;
using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;
using System.ComponentModel.Design;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/menuChoices")]
    public class MenuChoicesController : ControllerBase
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(MenuChoicesController));
        private MenuChoicesService _MenuChoicesService;

        public MenuChoicesController(MenuChoicesService menuChoicesService) 
        {
            _MenuChoicesService = menuChoicesService;
        }

        [Route("getAll")]
        [HttpGet]
        public IActionResult GetAll() 
        {
            try
            {
                _logger.Info("Trovando tutte le scelte...");
                List<MenuChoices> menuChoices = _MenuChoicesService.GetAll();
                if (menuChoices == null)
                {
                    _logger.Warn("Nessuna scelta trovata, NotFound");
                    return NotFound("scelta e' null");
                }
                _logger.Info("Scelte trovate, Ok");
                return Ok(menuChoices);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Error("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore Interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("getById")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                _logger.Info("Id scelta: " + id);
                _logger.Info("Prelevando una scelta mediante il suo id: " + id + "...");
                MenuChoices menuChoices = _MenuChoicesService.GetById(id);
                _logger.Info("Scelta trovata con successo!");
                return Ok(menuChoices);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Scelta non trovata: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("getByIdMenu")]
        [HttpGet]
        public IActionResult GetByIdMenu(int idMenu)
        {
            try
            {
                _logger.Info("Id menu: " + idMenu);
                _logger.Info("Prelevando tutte le scelte dato id di un menu: " + idMenu + "...");
                MenuChoices menuChoices = _MenuChoicesService.GetByIdMenu(idMenu);
                _logger.Info("Almeno una scelta trovata con successo!");
                return Ok(menuChoices);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Nessuna scelta trovata: " + ex.Message);
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
