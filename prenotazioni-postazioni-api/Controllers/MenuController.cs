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
    [Route("/api/menu")]
    public class MenuController : ControllerBase
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(MenuController));
        private MenuService _MenuService;

        public MenuController(MenuService menuService)
        {
            _MenuService = menuService;
        }
        [Route("getByDate")]
        [HttpGet]

        public IActionResult GetByDate(int year, int month, int day)
        {
            try
            {
                _logger.Info($"Year: {year}");
                _logger.Info("Month: " + month);
                _logger.Info("Day: " + day);
                _logger.Info("Trovando un menu mediante date..");
                Menu menu = _MenuService.GetByDate(new DateOnly ( year, month, day));
                if(menu == null)
                {
                    _logger.Warn("Menu e' null, return NotFound");
                    return NotFound("Menu è null");
                }
                _logger.Info("Menu non trovato. Return OK");
                return Ok(menu);
            }
            catch (PrenotazionePostazioniApiException ex) {
                _logger.Error("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message + "\nStack Trace:" + ex.StackTrace);
            }

        }

        [Route("getById")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                _logger.Info("Id menu: " + id);
                _logger.Info("Prelevando un menu mediante il suo id: " + id + "...");
                Menu menu = _MenuService.GetById(id);
                _logger.Info("Menu trovato con successo!");
                return Ok(menu);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Menu non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [Route("getAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _logger.Info("Trovando tutti i menu...");
                List<Menu> menu = _MenuService.GetAll();
                if(menu == null)
                {
                    _logger.Warn("Nessun menu trovato, NotFound");
                    return NotFound("menu e' null");
                }
                _logger.Info("Menu trovati, Ok");
                return Ok(menu);
            }
            catch(PrenotazionePostazioniApiException ex) {
                _logger.Error("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore Interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("addMenu")]
        [HttpPost]
        public IActionResult AddMenuByDate([FromBody] MenuDto menuDto)
        {
            try
            {
                _logger.Info("Giorno del menu: " + menuDto.Day);
                _logger.Info("salvando un menuDto nel database...");
                _MenuService.Save(menuDto);
                _logger.Info("MenuDto salvato con successo, OK");
                return Ok();
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Error("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("Bad request: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("deleteMenu")]
        [HttpDelete]

        public IActionResult DeleteMenu(int year, int month, int day)
        {
            try
            {
                DateOnly date = new DateOnly(year, month, day);
                _logger.Info("Giorno del menu: " + date);
                _MenuService.Delete(date);
                return Ok();
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


    }
}
