using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Repositories;
using prenotazioni_postazioni_api.Services;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/impostazioni")]
    public class ImpostazioneController : Controller
    {

        private ImpostazioneRepository impostazioneRepository = new ImpostazioneRepository();
        private ImpostazioneService impostazioneService = new ImpostazioneService();


        /// <summary>
        /// Serve per restituire lo stato dell'impostazione di emergenza
        /// </summary>
        /// <returns>Lo stato di emergenza. True o False</returns>
        [HttpGet]
        [Route("get-impostazione-emergenza")]
        public IActionResult GetImpostazioneEmergenza()
        {
            bool impostazione = impostazioneService.GetImpostazioneEmergenza();
            return Ok(impostazione);
        }

        /// <summary>
        /// Serve per cambiare lo stato delll'impostazione di emergenza
        /// </summary>
        /// <param name="userValue">
        /// il valore inserito dall'utente per cambiare l'impostazione di emergenza
        /// </param>
        [HttpPost]
        [Route("change-impostazione-emergenza")]
        public IActionResult ChangeImpostazioneEmergenza(bool userValue)
        {
            bool hasChanged = impostazioneService.ChangeImpostazioniEmergenza(userValue);
            if (hasChanged)
            {
                return Ok(hasChanged);
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
