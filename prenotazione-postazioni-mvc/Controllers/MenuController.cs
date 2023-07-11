using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_mvc.HttpServices;
using prenotazione_postazioni_mvc.Models;

namespace prenotazione_postazioni_mvc.Controllers
{
    public class MenuController : Controller
    {
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
        }

    }
}
