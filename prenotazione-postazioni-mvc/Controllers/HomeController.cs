using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_mvc.Models;

namespace prenotazione_postazioni_mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public static PrenotazioneViewModel ViewModel { get; set; }

    public IActionResult Index()
    {

        if (ViewModel == null)
            ViewModel = new PrenotazioneViewModel();

        return View(ViewModel);
    }

    /// <summary>
    ///     Cambia il giorno selezionato del Calendar.
    /// </summary>
    /// <param name="year">Anno selezionato</param>
    /// <param name="month">Mese selezionato</param>
    /// <param name="day">Giorno selezionato</param>
    /// <returns>RedirectToAction -> Index()</returns>

    [HttpPost]
    [ActionName("ReloadDay")]
    public IActionResult ReloadDay(int year, int month, int day)
    {
        //Dicembre

        if (month == 0)
        {
            month = 12;
            year--;
        }

        ViewModel.ChangeSelectedDay(year, month, day);

        return RedirectToAction("Index");
    }

    /// <summary>
    ///     Cambia la stanza selezionata dalla Map.
    /// </summary>
    /// <param name="room">Nome della Stanza selezionata</param>
    /// <returns>RedirectToAction -> Index()</returns>

    [HttpPost]
    [ActionName("ReloadRoom")]
    public IActionResult ReloadRoom(string room)
    {
        ViewModel.Stanza = room;

        return RedirectToAction("Index");
    }

    /// <summary>
    ///     Cambia il l'orario di inzio di una prenotazione.
    /// </summary>
    /// <param name="hour">Inizio selezionato</param>
    /// <returns>RedirectToAction -> Index()</returns>

    [HttpPost]
    [ActionName("ReloadStart")]
    public IActionResult ReloadStart(int hour)
    {
        ViewModel.ChangeSelectedStartHour(hour);

        return RedirectToAction("Index");
    }

    /// <summary>
    ///     Cambia il l'orario di termine di una prenotazione.
    /// </summary>
    /// <param name="hour">Termine selezionato</param>
    /// <returns>RedirectToAction -> Index()</returns>

    [HttpPost]
    [ActionName("ReloadFinish")]
    public IActionResult ReloadFinish(int hour)
    {
        ViewModel.ChangeSelectedEndHour(hour);

        return RedirectToAction("Index");
    }

    /// <summary>
    ///     Cambia lo stato del Collapse "#orario"
    /// </summary>
    /// <returns>Ok -> Collapse aggiornato</returns>

    [HttpPost]
    [ActionName("CollapseHour")]
    public IActionResult CollapseHour()
    {
        ViewModel.ToggleCollapseHour();

        return Ok("Collapse change");
    }

    /// <summary>
    ///     Cambia lo stato del Collapse "#prenotazioni"
    /// </summary>
    /// <returns>Ok -> Collapse aggiornato</returns>

    [HttpPost]
    [ActionName("CollapseList")]
    public IActionResult CollapseList()
    {
        ViewModel.ToggleCollapseList();

        return Ok("Collapse change");
    }
}
