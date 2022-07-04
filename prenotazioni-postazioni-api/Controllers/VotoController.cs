using Microsoft.AspNetCore.Mvc;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/voti")]
    public class VotoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
