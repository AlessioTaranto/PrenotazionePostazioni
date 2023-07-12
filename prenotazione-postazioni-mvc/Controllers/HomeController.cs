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

    public static BookingViewModel? ViewModel { get; set; }

    //HTTP Client Factory -> Prenotazioni
    public readonly BookingHttpSerivice _bookingHttpService;
    //HTTP Client Factory -> Festa
    public readonly HolidayHttpService _holidayHttpService;

    public HomeController(BookingHttpSerivice bookingHttpService, HolidayHttpService holidayHttpService)
    {
        _bookingHttpService = bookingHttpService;
        _holidayHttpService = holidayHttpService;
    }

    public IActionResult Index()
    {

        if (ViewModel == null)
            ViewModel = new BookingViewModel(_bookingHttpService,_holidayHttpService);

        //ReloadHoliday();

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
    [ActionName("ReloadDate")]
    public IActionResult ReloadDate(int year, int month, int day)
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
            ViewModel.Room = room;

        return Ok("Stanza selezionata");
    }

    /// <summary>
    ///     Cambia il l'orario di inzio di una prenotazione.
    /// </summary>
    /// <param name="hour">Inizio selezionato</param>
    /// <returns>Aggiorna orario di inizio</returns>

    [HttpPost]
    [ActionName("ReloadStartDate")]
    public IActionResult ReloadStartHour(int hour)
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
    [ActionName("ReloadEndHour")]
    public IActionResult ReloadEndHour(int hour)
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
    [ActionName("Booking")]
    public IActionResult Booking(string user, string room, string startDate, string endDate)
    {

        Task<HttpStatusCode>? getRq = ViewModel?.ExistBooking(user, room, startDate, endDate);
        getRq.Wait();

        HttpStatusCode code = getRq.Result;

        if (code == HttpStatusCode.NotFound)
        {
            //Non trova prenotazioni per quel giorno
            ViewModel?.DoBookingAsync(user, room, startDate, endDate);

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
    [ActionName("DeleteBooking")]
    public IActionResult DeleteBooking(string user, string room, string startDate, string endDate)
    {

        Task<Booking> bookingTask = ViewModel?.GetBooking(user, room, startDate, endDate);
        bookingTask.Wait();
        Booking? booking = bookingTask.Result;

        if (booking == null)
            return NotFound("Prenotazione non trovata");

        Task<HttpResponseMessage>? getRq = ViewModel?.Delete(booking.Id);
        getRq.Wait();
        HttpStatusCode code = getRq.Result.StatusCode;

        if (code == HttpStatusCode.OK)
            return Ok("Prenotazione cancellata");    
        else  
            return NotFound("Errore");
    }

    [HttpPost]
    [ActionName("GetAllBookedUser")]
    public IActionResult GetAllBookedUser(int startDate, int endDate)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Serve a caricare nel calendario le feste, [da aggiungere dove vi si trova un calendario]
    /// 
    /// TIP: Ricreare un modello apposito da implementare alle varie model
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ActionName("ReloadHoliday")]
    public IActionResult ReloadHoliday()
    {
        Task task = ViewModel.ReloadHoliday();
        task.Wait();

        return Ok("Festività ricaricate");
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
