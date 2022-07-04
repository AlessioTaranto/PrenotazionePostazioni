using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Repositories;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/impostazioni")]
    public class ImpostazioneController : Controller
    {

        private ImpostazioneRepository impostazioneRepository = ImpostazioneRepository.GetInstance();


        /// <summary>
        /// Serve per restituire lo stato dell'impostazione di emergenza
        /// </summary>
        /// <returns>Lo stato di emergenza. True o False</returns>
        [HttpGet]
        [Route("get-impostazione-emergenza")]
        public IActionResult GetImpostazioneEmergenza()
        {
            return Ok(true);
        }

        /// <summary>
        /// Serve per cambiare lo stato delll'impostazione di emergenza
        /// </summary>
        /// <param name="userValue">
        /// il valore inserito dall'utente per cambiare l'impostazione di emergenza
        /// </param>
        [HttpPost]
        [Route("change-impostazione-emergenza")]
        public void ChangeImpostazioneEmergenza(bool userValue)
        {
            
        }
    }
}
