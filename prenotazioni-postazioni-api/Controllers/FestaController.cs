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
        [HttpGet]
        public IActionResult GetAll()
        {

        }

        [Route("getAllByMonth")]
        [HttpGet]
        public IActionResult GetAllByMonth(int month)
        {

        }

        [Route("addFesta")]

        //[Route("deleteFestaByDate")]
    }
}
