using Hangfire;
using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using prenotazione_postazioni_mvc.Models;
using System.Net;
using System.Threading.Tasks;

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
            return View(ViewModel);
        }

        /*[HttpPost]
        [ActionName("sendMail")]
        public async Task<IActionResult> SendMail()
        {
            
            string destinatario = "Joeipaccini@gmail.com";
            string oggetto = "Prova";
            string corpo = "Ciao Rullooooooo";

            EmailUtility.InviaEmail(destinatario, oggetto, corpo);

            return RedirectToAction("Index");
        }*/

        [HttpPost]
        [ActionName("sendMail")]
        public async Task<IActionResult> SendEmail()
        {
            try
            {
                // Chiamata al metodo InviaEmail
                Task<HttpResponseMessage> getIdMenu = _menuHttpService.GetByDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                Menu? menu = await getIdMenu.Result.Content.ReadFromJsonAsync<Menu?>();
                Task<HttpResponseMessage> getAllChoice = _choicesHttpService.GetByIdMenu(menu.Id);
                List<MenuChoices>? menuChoices = null;
                menuChoices = await getAllChoice.Result.Content.ReadFromJsonAsync<List<MenuChoices>?>();
                string choices = "";
                foreach(MenuChoices mc in menuChoices) {
                    choices += mc.Choice + "\n";
                }
                EmailUtility.InviaEmail("andrix.braia@gmail.com", "Prova", choices);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Gestione dell'eccezione - puoi registrare l'errore, mostrare un messaggio di errore all'utente, ecc.
                // Per esempio:
                ViewBag.ErrorMessage = "Si è verificato un errore durante l'invio dell'email. Riprova più tardi.";
                return View("Error");
            }
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
           
            Task<HttpResponseMessage> add =  ViewModel.Add(choice, idUser, idMenu);
            add.Wait();
            HttpStatusCode code = add.Result.StatusCode;
            if(code == HttpStatusCode.OK) return Ok("scelta inviata");
            return BadRequest();
        }

        [HttpDelete]
        [ActionName("deleteChoice")]
        public IActionResult DeleteChoice(int idMenu, int idUser)
        {
            Task<HttpResponseMessage> delete = ViewModel.Delete(idMenu, idUser);
            delete.Wait();
            HttpStatusCode code = delete.Result.StatusCode;
            if (code == HttpStatusCode.OK) return Ok("scelta eliminata");
            return BadRequest();
        }


    }
}
