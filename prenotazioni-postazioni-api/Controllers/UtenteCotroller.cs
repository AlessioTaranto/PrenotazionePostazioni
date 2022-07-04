using Microsoft.AspNetCore.Mvc;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/utenti")]
    public class UtenteCotroller : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
