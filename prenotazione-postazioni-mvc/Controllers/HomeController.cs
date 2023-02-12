using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazione_postazioni_mvc.HttpServices;
using System.Net;

namespace prenotazione_postazioni_mvc.Controllers;

public class HomeController : Controller
{

    public static PrenotazioneViewModel? ViewModel { get; set; }

    //HTTP Client Factory -> Prenotazioni
    public readonly PrenotazioneHttpSerivice _prenotazioneHttpService;

    public HomeController(PrenotazioneHttpSerivice prenotazioneHttpService)
    {
        _prenotazioneHttpService = prenotazioneHttpService;
    }

    public IActionResult Index()
    {

        if (ViewModel == null)
            ViewModel = new PrenotazioneViewModel(_prenotazioneHttpService);

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


    [HttpPost]
    [ActionName("Prenota")]
    public IActionResult Prenota(string user, string room, string start, string end)
    {

        Task<HttpStatusCode>? getRq = ViewModel?.ExistsPrenotazione(user, room, start, end);
        getRq.Wait();

        HttpStatusCode code = getRq.Result;

        if (code == HttpStatusCode.NotFound)
        {
            //Non trova prenotazioni per quel giorno
            ViewModel?.doPrenotazioneAsync(user, room, start, end);

            return Ok("Prenotazione effettuata");
        }
        else if (code == HttpStatusCode.OK)
        {
            return NotFound("Prenotazione già effettuata");
        }
        else
        {
            return BadRequest("Errore");
        }
    }

    [HttpGet]
    [ActionName("DeletePrenotazione")]
    public IActionResult DeletePrenotazione(string user, string room, string start, string end)
    {

        Task<Prenotazione> prenotazioneTask = ViewModel?.GetPrenotazione(user, room, start, end);
        prenotazioneTask.Wait();
        Prenotazione? prenotazione = prenotazioneTask.Result;

        if (prenotazione == null)
            return NotFound("Prenotazione non trovata");

        Task<HttpResponseMessage>? getRq = ViewModel?.DeletePrenotazione(prenotazione.IdPrenotazioni);
        getRq.Wait();
        HttpStatusCode code = getRq.Result.StatusCode;

        if (code == HttpStatusCode.OK)
            return Ok("Prenotazione cancellata");    
        else  
            return NotFound("Errore");
    }

    [HttpPost]
    [ActionName("GetAllUtentiPrenotazione")]
    public IActionResult GetAllPersonePrenotate(int inizio, int fine)
    {
        throw new NotImplementedException();
    }

    //TESTING
    
    /*[ActionName("Login")]
    public async Task Login()
    {
        await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
        {
            RedirectUri = Url.Action("GoogleResponse")
        });
    }

    [ActionName("GoogleResponse")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var claims = result.Principal.Identities
            .FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });
        return Json(claims);
    }

    [Authorize]
    [ActionName("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index");
    }*/
}
