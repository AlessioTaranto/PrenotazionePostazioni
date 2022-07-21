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

        public static ImpostazioniViewModel? ViewModel { get; set; }

        /// <summary>
        ///     Cambia la stanza selezionata nel tab "Covid / Capienza"
        /// </summary>
        /// <param name="stanza">Stanza selezionata</param>
        /// <returns>RedirectToAction -> Index()</returns>

        [HttpPost]
        [ActionName("SelectRoom")]
        public IActionResult SelectRoom(string stanza)
        {
            if (ViewModel != null)
                ViewModel.CapienzaViewModel.Stanza = stanza;

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Cambia il giorno della festività selezionata nel Calendar al tab "Festività"
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <returns>RedirectToAction -> Index()</returns>

        [HttpPost]
        [ActionName("SelectFesta")]
        public IActionResult SelectFesta(int year, int month, int day)
        {
            // Dicembre
            if (month == 0)
            {
                month = 12;
                year--;
            }

            ViewModel?.FestivitaViewModel.SelectFesta(year, month, day);

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Aggiungi una festività, in base al giorno selezionato, nel tab "Festività"
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <returns>RedirectToAction -> Index()</returns>

        [HttpPost]
        [ActionName("AggiungiFesta")]
        public IActionResult AggiungiFesta(int year, int month, int day)
        {
            ViewModel?.FestivitaViewModel.AddFesta(year, month, day);

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Rimuovi una festività, in base al giorno selezionato, nel tab "Festività"
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <returns>RedirectToAction -> Index()</returns>

        [HttpPost]
        [ActionName("RimuoviFesta")]
        public IActionResult RimuoviFesta(int year, int month, int day)
        {
            ViewModel?.FestivitaViewModel.RemoveFesta(year, month, day);

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Cambia lo stato del Tab
        /// </summary>
        /// <param name="number">Id tab</param>
        /// <returns>Ok -> Tab aggiornato</returns>

        [HttpPost]
        [ActionName("ChangeStateTab")]
        public IActionResult ChangeStateTab(int number)
        {
            if (ViewModel != null)
                ViewModel.StateTab = number;

            return Ok("Tab changed");
        }

        /// <summary>
        ///     Cambia lo stato del Collapse delle presenze, nel tab "Presenze"
        /// </summary>
        /// <returns>Ok -> Collapse aggiornato</returns>

        [HttpPost]
        [ActionName("CollapsePresenze")]
        public IActionResult CollapsePresenze()
        {
            ViewModel?.PresenzeViewModel.ToggleCollapseList();

            return Ok("Collapse changed");
        }

        /// <summary>
        ///     Seleziona un giorno del Calendar1 delle presenze, nel tab "Presenze"
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese Selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <returns>RedirectToAction -> Index()</returns>

        [HttpPost]
        [ActionName("SelectPresenza")]
        public IActionResult SelectPresenza(int year, int month, int day)
        {
            if (month == 0)
            {
                month = 12;
                year--;
            }

            ViewModel?.PresenzeViewModel.SelectPresenza(year, month, day);

            return RedirectToAction("Index");
        }

    }
}
