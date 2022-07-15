using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_mvc.Models;

namespace prenotazione_postazioni_mvc.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        PrenotazioneViewModel prenotazione = new PrenotazioneViewModel();

        return View(prenotazione);
    }

    public IActionResult SelectDay(DateTime date)
    {
        PrenotazioneViewModel prenotazione = new PrenotazioneViewModel(date, "");

        return View(prenotazione);
    }

    public IActionResult SelectStanza(DateTime date, string stanza)
    {
        PrenotazioneViewModel prenotazione = new PrenotazioneViewModel(date, stanza);

        return View(prenotazione);
    }

    public IActionResult SelectStanza(DateTime date, string stanza, DateTime start, DateTime end)
    {
        PrenotazioneViewModel prenotazione = new PrenotazioneViewModel(date, stanza, start, end);

        return View(prenotazione);
    }
}
