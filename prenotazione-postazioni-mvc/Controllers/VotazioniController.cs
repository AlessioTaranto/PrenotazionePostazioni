using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_mvc.Models;

namespace prenotazione_postazioni_mvc.Controllers
{
    public class VotazioniController : Controller
    {

        public static VotazioniViewModel ViewModel { get; set; }

        public IActionResult Index()
        {
            if(ViewModel == null)
                ViewModel = new VotazioniViewModel();
            return View(ViewModel);
        }

        [HttpPost]
        [ActionName("VoteUser")]
        public IActionResult VoteUser(bool? voto, int i)
        {
            try
            {
                ViewModel.Votazioni[i] = voto;
            } catch(Exception)
            {
                ViewModel.Votazioni.Add(voto);
            }

            return RedirectToAction("Index");
        }
    }
}
