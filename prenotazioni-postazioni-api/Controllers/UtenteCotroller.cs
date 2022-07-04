using Microsoft.AspNetCore.Mvc;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/utenti")]
    public class UtenteCotroller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
