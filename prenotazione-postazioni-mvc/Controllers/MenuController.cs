using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using prenotazione_postazioni_mvc.Models;
using System.Net;

namespace prenotazione_postazioni_mvc.Controllers
{
    public class MenuController : Controller
    {
        public readonly MenuHttpService _menuHttpService;
        public readonly MenuChoicesHttpService _choicesHttpService;
        public readonly HolidayHttpService _holidayHttpService;
        public readonly UserHttpService _userHttpService;

        public MenuController(MenuHttpService menuHttpService, MenuChoicesHttpService menuChoicesHttpService, HolidayHttpService holidayHttpService, UserHttpService userHttpService)
        {
            _menuHttpService = menuHttpService;
            _choicesHttpService = menuChoicesHttpService;
            _holidayHttpService = holidayHttpService;
            _userHttpService = userHttpService;
        }

        public static MenuViewModel ViewModel { get; set; }
        public IActionResult Index()
        {
            if (ViewModel == null)
                ViewModel = new MenuViewModel(_menuHttpService, _choicesHttpService, _holidayHttpService, _userHttpService);
            ReloadHoliday();
            ReloadMenu();
            //ViewModel.Menu = new Menu();
            return View(ViewModel);
        }

        [HttpGet]
        [ActionName("reloadMenu")]
        public IActionResult ReloadMenu()
        {
            Task task = ViewModel.ReloadMenu();
            task.Wait();
            return Ok("Menu caricato");
        }
        [HttpGet]
        [ActionName("reloadHoliday")]
        public IActionResult ReloadHoliday()
        {
            Task task = ViewModel.ReloadHoliday();
            task.Wait();
            return Ok("festivita caricate");
        }

        [HttpPost]
        [ActionName("addChoice")]
        public IActionResult AddChoice(string choice, int idUser, int idMenu)
        {
            Task<HttpStatusCode> getRq = ViewModel.ExistsChoice(idMenu, idUser);
            getRq.Wait();
            HttpStatusCode code = getRq.Result;
            if (code == HttpStatusCode.OK)
            {
                //codice per modificare la scelta dell'user quando trova la scelta gia effettuata
                ViewModel.Delete(idMenu, idUser);
            } 
            else if(code == HttpStatusCode.BadRequest)
            {
                return BadRequest();
            }
                ViewModel.Add(choice, idUser, idMenu);
                return Ok("scelta inviata");

        }

        //[HttpDelete]
        //[ActionName("removeChoice")]
        //public IActionResult DeleteChoice(int idMenu, int idUser) 
        //{
        //    ViewModel.Delete(idMenu, idUser);
        //    return Ok("scelta eliminata");
        //}
    }
}
