using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_mvc.Models;

namespace prenotazione_postazioni_mvc.Controllers
{
    public class ImpostazioniController : Controller
    {
        public IActionResult Index()
        {
            if (ViewModel == null)
                ViewModel = new ImpostazioniViewModel(
                    new CapienzaImpostazioniViewModel(), 
                    new FestivitaImpostazioniViewModel(), 
                    new PresenzeImpostazioniViewModel()
                );

            return View(ViewModel);
        }

        public static ImpostazioniViewModel ViewModel;

        [HttpPost]
        [ActionName("SelectRoom")]
        public IActionResult SelectRoom(string stanza)
        {
            ViewModel.CapienzaViewModel.Stanza = stanza;

            return RedirectToAction("Index");
        }


    }
}
