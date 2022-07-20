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

    public IActionResult Index()
    {

        PrenotazioneViewModel prenotazione = HttpContext.Session.GetObjectFromJson<PrenotazioneViewModel>("PrenotazioneViewModel");

        if (prenotazione == null)
            prenotazione = new PrenotazioneViewModel();

        HttpContext.Session.SetObjectAsJson("PrenotazioneViewModel", prenotazione);
        return View(prenotazione);
    }

    [HttpPost]
    [ActionName("ReloadDay")]
    public IActionResult ReloadDay(int year, int month, int day)
    {

        PrenotazioneViewModel prenotazione = HttpContext.Session.GetObjectFromJson<PrenotazioneViewModel>("PrenotazioneViewModel");

        prenotazione.Date = new DateTime(year, month, day);

        HttpContext.Session.SetObjectAsJson("PrenotazioneViewModel", prenotazione);
        return View(prenotazione);
    }
}
