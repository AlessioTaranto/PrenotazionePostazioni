using Microsoft.AspNetCore.Mvc;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/festivita")]
    public class FestaController : ControllerBase
    {
        [Route("getByDate")]
        [HttpGet]
        public IActionResult GetByDate(DateOnly date)
        {
            return Ok();
        }

        [Route("getAll")]


        [Route("getAllByMonth")]

        [Route("addFestivita")]
    }
}
