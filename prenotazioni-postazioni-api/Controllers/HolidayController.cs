using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using prenotazioni_postazioni_api.Services;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/holiday")]
    public class HolidayController : ControllerBase
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(HolidayController));
        private HolidayService _holidayService;

        public HolidayController(HolidayService holidayService)
        {
            this._holidayService = holidayService;
        }





        /// <summary>
        /// Restituisce tutte le feste di un giorno
        /// </summary>
        /// <param name="date">Il giorno </param>
        /// <returns>Lista di feste trovate</returns>

        [Route("getByDate")]
        [HttpGet]
        public IActionResult GetByDate(int year, int month, int day)
        {
            try
            {
                logger.Info($"Year: {year}");
                logger.Info("Month: " + month);
                logger.Info("Day: " + day);
                logger.Info("Trovando una festa mediante date...");
                Holiday holiday = _holidayService.GetByDate(new DateTime(year, month, day));
                if(holiday == null)
                {
                    logger.Warn("Festa e' null, return NotFound");
                    return NotFound("Festa è null");
                }
                logger.Info("Festa trovato. Return OK");
                return Ok(holiday);
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                logger.Error("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message+"\nStack Trace:"+ex.StackTrace);
            }
            
        }
        /// <summary>
        /// Restituisce tutte le feste fatte
        /// </summary>
        /// <returns>Lista di tutte le feste trovate</returns>
        [Route("getAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                logger.Info("Trovando tutte le feste...");
                List<Holiday> holidays = _holidayService.GetAll();
                if(holidays == null)
                {
                    logger.Warn("Nessuna festa trovata, NotFound");
                    return NotFound("feste e' null");
                }
                logger.Info("Feste trovate, Ok");
                return Ok(holidays);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                logger.Error("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Fatal("Errore Interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add([FromBody] HolidayDto holidayDto)
        {
            try
            {
                logger.Info("Giorno della festa: " + holidayDto.Date);
                logger.Info("Descrizione della festa: " + holidayDto.Description);
                logger.Info("Salvando una festaDto del database...");
                Console.WriteLine("HolidayDto" + holidayDto.Date);
                _holidayService.Add(holidayDto);
                logger.Info("HolidayDto salvato con successo, Ok");
                return Ok();
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                logger.Error("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Fatal("Errore Interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("delete")]
        [HttpDelete]
        public IActionResult Delete(int year, int month, int day) { 
            try
            {
                DateTime date = new DateTime(year, month, day);

                logger.Info("Giorno della festa: " + date);
                _holidayService.Delete(date);
                return Ok();
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                logger.Error("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Fatal("Errore Interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

    }
}
