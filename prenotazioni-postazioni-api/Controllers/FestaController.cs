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


        [Route("getByDate")]
        [HttpGet]
        public IActionResult GetByDate(DateOnly date)
        {
            try
            {
                Festa festa = _festaService.GetByDate(date);
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

        [Route("getAllByMonth")]
        [HttpGet]
        public IActionResult GetAllByMonth(int month)
        {
            try
            {
                List<Feste> festeByMonth = _festaService.GetAllByMonth(month);
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
