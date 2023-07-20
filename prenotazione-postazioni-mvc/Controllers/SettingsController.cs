using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using prenotazione_postazioni_mvc.Models;

namespace prenotazione_postazioni_mvc.Controllers
{
    public class SettingsController : Controller
    {
        //HTTP Client Factory -> Capienza
        public readonly CapacityHttpService _capacityHttpService;
        //HTTP Client Factory -> Festa
        public readonly HolidayHttpService _festaHttpService;
        public readonly RoleHttpService _roleHttpService;
        public readonly MenuHttpService _menuHttpService;

        public int numero = 0;

        public SettingsController(CapacityHttpService capacityHttpService, HolidayHttpService holidayHttpService, RoleHttpService rolehttpService)
        {
            _capacityHttpService = capacityHttpService;
            _festaHttpService = holidayHttpService;
            _roleHttpService = rolehttpService;
        }

        public IActionResult Index()
        {
            if (ViewModel == null)
                ViewModel = new SettingsViewModel(
                    new CapacitySettingsViewModel(_capacityHttpService), 
                    new AttendanceSettingsViewModel(),
                    _festaHttpService
                );

            ReloadHoliday();

            return View(ViewModel);
        }

        [HttpPost]
        [ActionName("SaveImage")]
        public void SaveImage(string image)
        {
            image = image.Substring(image.IndexOf(",")+1);
            byte[] byteArray = Convert.FromBase64String(image);
            _menuHttpService.Add(DateTime.Now, byteArray);
        }


        public static SettingsViewModel? ViewModel { get; set; }

        [HttpPut]
        [ActionName("UpdateRole")]
        public void UpdateRole(int idUser, string futureRole){
            _roleHttpService.Update(idUser, futureRole);
        }

        /// <summary>
        ///     Cambia la stanza selezionata nel tab "Covid / Capienza"
        /// </summary>
        /// <param name="room">Stanza selezionata</param>
        /// <returns>RedirectToAction -> Index()</returns>

        [HttpPost]
        [ActionName("SelectRoom")]
        public IActionResult SelectRoom(string room)
        {
            if (ViewModel != null)
                ViewModel.CapacitySettingsViewModel.Room = room;

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Cambia il giorno della festività selezionata nel Calendar al tab "Festività"
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <returns>RedirectToAction -> Index()</returns>

        [HttpPost]
        [ActionName("SelectHoliday")]
        public IActionResult SelectFesta(int year, int month, int day)
        {
            // Dicembre
            if (month == 0)
            {
                month = 12;
                year--;
            }

            ViewModel?.SetHolidaySelected(year, month, day);

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Aggiungi una festività, in base al giorno selezionato, nel tab "Festività"
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <returns>RedirectToAction -> Index()</returns>

        [HttpPost]
        [ActionName("AddHoliday")]
        public IActionResult AddHoliday(int year, int month, int day, string description)
        {
            Console.WriteLine("Year: " + year + " - Month: " + month + " - day: " + day);
            ViewModel?.AddHoliday(year, month, day, description);

            return Ok();
        }

        /// <summary>
        ///     Rimuovi una festività, in base al giorno selezionato, nel tab "Festività"
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <returns>RedirectToAction -> Index()</returns>
        
        [HttpDelete]
        [ActionName("DeleteHoliday")]
        public IActionResult DeleteHoliday(int year, int month, int day)
        {
            ViewModel?.DeleteHoliday(year, month, day);

            return Ok();
        }

        /// <summary>
        ///     Cambia lo stato del Tab
        /// </summary>
        /// <param name="number">Id tab</param>
        /// <returns>Ok -> Tab aggiornato</returns>

        [HttpPost]
        [ActionName("ChangeStateTab")]
        public IActionResult ChangeStateTab(int number)
        {
            if (ViewModel != null)
                ViewModel.StateTab = number;

            return Ok("Tab changed");
        }

        /// <summary>
        ///     Cambia lo stato del Collapse delle presenze, nel tab "Presenze"
        /// </summary>
        /// <returns>Ok -> Collapse aggiornato</returns>

        [HttpPost]
        [ActionName("CollapseAttendance")]
        public IActionResult CollapseAttendance()
        {
            ViewModel?.AttendanceSettingsViewModel.ToggleCollapseList();

            return Ok("Collapse changed");
        }

        /// <summary>
        ///     Seleziona un giorno del Calendar1 delle presenze, nel tab "Presenze"
        /// </summary>
        /// <param name="year">Anno selezionato</param>
        /// <param name="month">Mese Selezionato</param>
        /// <param name="day">Giorno selezionato</param>
        /// <returns>RedirectToAction -> Index()</returns>

        [HttpPost]
        [ActionName("SelectAttendance")]
        public IActionResult SelectAttendance(int year, int month, int day)
        {
            if (month == 0)
            {
                month = 12;
                year--;
            }

            ViewModel?.AttendanceSettingsViewModel.SelectAttendance(year, month, day);

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Abilita / Disabilita la modalità covid
        /// </summary>
        /// <returns>Ok -> Modalità cambiata</returns>

        [HttpPost]
        [ActionName("ToggleModEmergency")]
        public IActionResult ToggleModEmergency()
        {

            if (_capacityHttpService == null)
                return BadRequest("Errore generico");

            _capacityHttpService.ToggleModEmergency();

            return Ok("Modalità cambiata");
        }

        /// <summary>
        ///     Imposta la capienza normale di una stanza 
        /// </summary>
        /// <param name="room">Stanza selezionata</param>
        /// <param name="capacity">Capienza selezionata</param>
        /// <returns>Ok -> Capienza aggiornata</returns>

        [HttpPost]
        [ActionName("ReloadCapacity")]
        public IActionResult ReloadCapacity(string room, int capacity)
        {
            try
            {
                ViewModel.CapacitySettingsViewModel.SetCapacity(room, capacity);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok("Capienza aggiornata");
        }

        /// <summary>
        ///     Imposta la capienza covid di una stanza
        /// </summary>
        /// <param name="room">Stanza selezionata</param>
        /// <param name="capacity">Capienza selezionata</param>
        /// <returns>Ok -> Capienza aggiornata</returns>

        [HttpPost]
        [ActionName("ReloadCapacityEmergency")]
        public IActionResult ReladCapacityEmergency(string room, int capacity)
        {
            try
            {
                ViewModel.CapacitySettingsViewModel.SetCapacityEmergency(room, capacity);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok("Capienza aggiornata");
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
    }
}
