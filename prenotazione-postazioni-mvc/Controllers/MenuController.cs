using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using prenotazione_postazioni_mvc.Models;
using System.Net;
=======
using prenotazione_postazioni_mvc.HttpServices;
using prenotazione_postazioni_mvc.Models;
>>>>>>> fde4092c1c88d66dc32d312a0639cbf8937167cf

namespace prenotazione_postazioni_mvc.Controllers
{
    public class MenuController : Controller
    {
<<<<<<< HEAD
        public readonly MenuHttpService _menuHttpService;
        public readonly MenuChoicesHttpService _choicesHttpService;

        public MenuController(MenuHttpService menuHttpService, MenuChoicesHttpService menuChoicesHttpService)
        {
            _menuHttpService = menuHttpService;
            _choicesHttpService = menuChoicesHttpService;
        }

        public static MenuViewModel ViewModel { get; set; }
        public IActionResult Index()
        {
            if (ViewModel == null)
                ViewModel = new MenuViewModel(_menuHttpService, _choicesHttpService);
            ReloadMenu();
            ViewModel.Menu.Image = "https://marketplace.canva.com/EADrKskBfV8/1/0/1236w/canva-oro-festa-di-san-valentino-cibo-e-bevande-menu-6TIe65doazo.jpg";

            return View(ViewModel);
        }

        [HttpGet]
        [ActionName("reloadMenu")]
        public IActionResult ReloadMenu()
        {
            Task task = ViewModel.ReLoadMenu();
            task.Wait();
            return Ok("Menu caricato");
        }

        [HttpPost]
        [ActionName("addChoice")]
        public IActionResult AddChoice(string choice, int idUser, int idMenu)
        {
           return Ok("scelta gia effettuata");


            //Task<HttpStatusCode> getRq = ViewModel.ExistsChoice(idMenu, idUser);
            //getRq.Wait();
            //HttpStatusCode code = getRq.Result;
            //if (code == HttpStatusCode.OK)
            //{
            //    //codice per modificare la scelta dell'user quando trova la scelta gia effettuata
            //    Ok("scelta gia effettuata");

            //}
            //else if (code == HttpStatusCode.NotFound)
            //{
            //    //codice per fare la scelta 
            //    ViewModel.Add(choice, idUser,idMenu);
            //    return Ok("scelta inviata");
            //}
            //return BadRequest();
=======
        public static MenuViewModel? ViewModel { get; set; }
        public readonly MenuHttpService _menuHttpService;
        public readonly MenuChoicesHttpService _menuChoicesHttpService;
        public readonly HolidayHttpService _holidayHttpService;


        public MenuController(MenuHttpService _menuHttpService, MenuChoicesHttpService _menuChoicesHttpService, HolidayHttpService _holidayHttpService)

        {
            _menuHttpService = _menuHttpService;
            _menuChoicesHttpService = _menuChoicesHttpService;
            _holidayHttpService = _holidayHttpService;

        }
        public IActionResult Index()
        {
            return View();
>>>>>>> fde4092c1c88d66dc32d312a0639cbf8937167cf
        }

    }
}
