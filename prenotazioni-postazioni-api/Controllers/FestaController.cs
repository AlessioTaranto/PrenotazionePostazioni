using Microsoft.AspNetCore.Mvc;
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
        private FestaService _festaService = new FestaService();
        /// <summary>
        /// Restituisce tutte le feste di un giorno
        /// </summary>
        /// <param name="date">Il giorno </param>
        /// <returns>Lista di feste trovate</returns>

        [Route("getByDate")]
        [HttpGet]
        public IActionResult GetByDate(DateOnly date)
        {
            try
            {
                List<Festa> feste = _festaService.GetByDate(date);
                if(feste == null)
                {
                    return NotFound("Festa e' null");
                }
                return Ok(feste);
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
        public IActionResult AddFestaByDate(int year, int month, int day, string desc)
        {
            try
            {
                _festaService.Save(new DateOnly(year, month, day), desc);
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
