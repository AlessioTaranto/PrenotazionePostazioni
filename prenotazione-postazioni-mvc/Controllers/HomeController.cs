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

    public IActionResult ReloadDay(DateTime date, PrenotazioneViewModel prenotazione)
    {
        prenotazione = new PrenotazioneViewModel(date, prenotazione.Stanza, prenotazione.Start, prenotazione.End);

        return View(prenotazione);
    }

    public IActionResult ReloadStanza(string stanza, PrenotazioneViewModel prenotazione)
    {
        prenotazione = new PrenotazioneViewModel(prenotazione.Date, stanza, prenotazione.Start, prenotazione.End);

        return View(prenotazione);
    }
}
