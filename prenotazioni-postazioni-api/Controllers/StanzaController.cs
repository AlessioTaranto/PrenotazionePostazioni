using Microsoft.AspNetCore.Mvc;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/stanze")]
    public class StanzaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
