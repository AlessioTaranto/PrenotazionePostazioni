using Microsoft.AspNetCore.Mvc;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/ruoli")]
    public class RuoloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
