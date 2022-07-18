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
            logger.Log("INFO", "Inizio GetByDate");
            try
            {
                Festa festa = _festaService.GetByDate(new DateTime(year, month, day));
                if(festa == null)
                {
                    return NotFound("Festa è null");
                }
                return Ok(festa);
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
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
                List<Festa> feste = _festaService.GetAll();
                if(feste == null)
                {
                    return NotFound("feste e' null");
                }
                return Ok(feste);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("addFesta")]
        [HttpGet]
        public IActionResult AddFestaByDate([FromBody] FestaDto festaDto)
        {
            try
            {
                _festaService.Save(festaDto);
                return Ok();
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[Route("deleteFestaByDate")]
    }
}
