using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using prenotazioni_postazioni_api.Services;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/festivita")]
    public class FestaController : ControllerBase
    {
        private readonly ILogger<FestaController> logger;
        private FestaService _festaService = new FestaService();

        public FestaController(ILogger<FestaController> logger)
        {
            this.logger = logger;
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
                logger.LogInformation("Trovando una festa mediante date...");
                Festa festa = _festaService.GetByDate(new DateTime(year, month, day));
                if(festa == null)
                {
                    logger.LogWarning("Festa e' null, return NotFound");
                    return NotFound("Festa è null");
                }
                logger.LogInformation("Festa trovato. Return OK");
                return Ok(festa);
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                logger.LogError(ex.Message + " Return Bad Request");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.Message + " Errore Interno, Return 500");
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
                logger.LogInformation("Trovando tutte le feste...");
                List<Festa> feste = _festaService.GetAll();
                if(feste == null)
                {
                    logger.LogWarning("Nessuna festa trovata, NotFound");
                    return NotFound("feste e' null");
                }
                logger.LogInformation("Feste trovate, Ok");
                return Ok(feste);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                logger.LogError(ex.Message + "PrenotazionePostazioniApiException, BadRequest");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.Message + "Errore Interno, 500");
                return StatusCode(500, ex.Message);
            }
        }

        [Route("addFesta")]
        [HttpGet]
        public IActionResult AddFestaByDate([FromBody] FestaDto festaDto)
        {
            try
            {
                logger.LogInformation("Salvando una festaDto del database...");
                _festaService.Save(festaDto);
                logger.LogInformation("FestaDto salvato con successo, Ok");
                return Ok();
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                logger.LogError(ex.Message + " PrenotazionePostazioniApiException, bad request");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.Message + " errore interno, 500")
                return StatusCode(500, ex.Message);
            }
        }

        //[Route("deleteFestaByDate")]
    }
}
