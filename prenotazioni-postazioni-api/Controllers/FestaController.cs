using Microsoft.AspNetCore.Mvc;
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
                if(festa == null)
                {
                    return NotFound("Festa e' null");
                }
                return Ok(festa);
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

        /// <summary>
        /// Restituisce tutte le feste di un mese
        /// </summary>
        /// <param name="month">Il mese delle feste</param>
        /// <returns>Lista di feste</returns>
        [Route("getAllByMonth")]
        [HttpGet]
        public IActionResult GetAllByMonth(int month)
        {
            try
            {
                List<Festa> festeByMonth = _festaService.GetAllByMonth(month);
                if(festeByMonth == null)
                {
                    return NotFound("Feste by month e' null");
                }
                return Ok(festeByMonth);
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("addFesta")]

        //[Route("deleteFestaByDate")]
    }
}
