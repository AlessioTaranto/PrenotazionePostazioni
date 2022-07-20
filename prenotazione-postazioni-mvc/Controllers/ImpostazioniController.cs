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

        [HttpPost]
        [ActionName("SelectFesta")]
        public IActionResult SelectFesta(int year, int month, int day)
        {

            if (month == 0)
            {
                month = 12;
                year--;
            }

            ViewModel.FestivitaViewModel.GiornoSelezionato = new DateTime(year, month, day);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("AggiungiFesta")]
        public IActionResult AggiungiFesta(int year, int month, int day)
        {
            ViewModel.FestivitaViewModel.AddFesta(new DateTime(year, month, day));

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("RimuoviFesta")]
        public IActionResult RimuoviFesta(int year, int month, int day)
        {
            ViewModel.FestivitaViewModel.RemoveFesta(new DateTime(year, month, day));

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("ChangeStateTab")]
        public IActionResult ChangeStateTab(int number)
        {
            ViewModel.StateTab = number;

            return Ok("Tab changed");
        }

        [HttpPost]
        [ActionName("CollapsePresenze")]
        public IActionResult CollapsePresenze()
        {
            ViewModel.PresenzeViewModel.ToggleCollapseList();

            return Ok("Collapse changed");
        }

        [HttpPost]
        [ActionName("SelectPresenza")]
        public IActionResult SelectPresenza(int year, int month, int day)
        {
            if (month == 0)
            {
                month = 12;
                year--;
            }

            ViewModel.PresenzeViewModel.PresenzaSelezionata = new DateTime(year, month, day);

            return RedirectToAction("Index");
        }

    }
}
