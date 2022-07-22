using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using prenotazione_postazioni_mvc.Models;

namespace prenotazione_postazioni_mvc.Controllers;

public class HomeController : Controller
{

    public static PrenotazioneViewModel ViewModel { get; set; }
    private StanzeHttpService stanzeHttpService;
    private UtenteHttpService utenteHttpService;
    private PrenotazioniHttpService prenotazioniHttpService;

    public HomeController(StanzeHttpService stanzeHttpService, UtenteHttpService utenteHttpService, PrenotazioniHttpService prenotazioniHttpService)
    {
        this.stanzeHttpService = stanzeHttpService;
        this.utenteHttpService = utenteHttpService;
        this.prenotazioniHttpService = prenotazioniHttpService;
    }

    public IActionResult Index()
    {

        if (ViewModel == null)
            ViewModel = new PrenotazioneViewModel(stanzeHttpService, prenotazioniHttpService, utenteHttpService);

        return View(ViewModel);
    }

    /// <summary>
    ///     Cambia il giorno selezionato del Calendar.
    /// </summary>
    /// <param name="year">Anno selezionato</param>
    /// <param name="month">Mese selezionato</param>
    /// <param name="day">Giorno selezionato</param>
    /// <returns>Giorno aggiornato</returns>

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

        try
        {
            ViewModel?.ChangeSelectedDay(year, month, day);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok("Giorno selezionato");
    }

    /// <summary>
    ///     Cambia la stanza selezionata dalla Map.
    /// </summary>
    /// <param name="room">Nome della Stanza selezionata</param>
    /// <returns>Stanza aggiornata</returns>

    [HttpPost]
    [ActionName("ReloadRoom")]
    public IActionResult ReloadRoom(string room)
    {
        if (ViewModel != null)
            ViewModel.Stanza = room;

        return Ok("Stanza selezionata");
    }

    /// <summary>
    ///     Cambia il l'orario di inzio di una prenotazione.
    /// </summary>
    /// <param name="hour">Inizio selezionato</param>
    /// <returns>Aggiorna orario di inizio</returns>

    [HttpPost]
    [ActionName("ReloadStart")]
    public IActionResult ReloadStart(int hour)
    {
        try
        {
            ViewModel?.ChangeSelectedStartHour(hour);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok("Orario d'inizio aggiornato");
    }

    /// <summary>
    ///     Cambia il l'orario di termine di una prenotazione.
    /// </summary>
    /// <param name="hour">Termine selezionato</param>
    /// <returns>Orario di termine aggiornato</returns>

    [HttpPost]
    [ActionName("ReloadFinish")]
    public IActionResult ReloadFinish(int hour)
    {
        try
        {
            ViewModel?.ChangeSelectedEndHour(hour);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok("Orario di termine aggiornato");
    }

    /// <summary>
    ///     Cambia lo stato del Collapse "#orario"
    /// </summary>
    /// <returns>Ok -> Collapse aggiornato</returns>

    [HttpPost]
    [ActionName("CollapseHour")]
    public IActionResult CollapseHour()
    {
        ViewModel?.ToggleCollapseHour();

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
        ViewModel?.ToggleCollapseList();

        return Ok("Collapse change");
    }

    
}

