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

    [HttpPost]
    [ActionName("ReloadDay")]
    public IActionResult ReloadDay(int year, int month, int day)
    {

        ViewModel.Date = new DateTime(year, month, day);

        return Ok("Success");
    }

    [HttpPost]
    [ActionName("ReloadRoom")]
    public IActionResult ReloadRoom(string room)
    {
        ViewModel.Stanza = room;

        return RedirectToAction("Index");
    }
}
