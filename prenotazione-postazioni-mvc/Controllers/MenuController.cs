using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using prenotazione_postazioni_mvc.Models;
using System.Net;
namespace prenotazione_postazioni_mvc.Controllers
{
    public class MenuController : Controller
    {
        public static MenuViewModel ViewModel { get; set; }
        public readonly MenuHttpService _menuHttpService;
        

        public MenuController(MenuHttpService menuHttpService)
        {
            _menuHttpService = menuHttpService;
            
        }

        public IActionResult Index()
        {
            return View();

        }



    }
}
