using Newtonsoft.Json;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using static System.Net.WebRequestMethods;

namespace prenotazione_postazioni_mvc.Models
{
    public class MenuViewModel
    {
        public DateTime Date { get; set; }
        public string DescriptionHoliday { get; set; }
        public Menu Menu { get; set; }
        public MenuChoices MenuChoice { get; set; }
        public List<Holiday> Holidays { get; set; }

        public readonly MenuHttpService _menuHttpService;
        public readonly MenuChoicesHttpService _choicesHttpService;
        public readonly HolidayHttpService _holidayHttpService;
        public readonly UserHttpService _userHttpService;


        public MenuViewModel(MenuHttpService menuHttpService, MenuChoicesHttpService menuChoicesHttpService, HolidayHttpService holidayHttpService, UserHttpService userHttpService)
        {
            _menuHttpService = menuHttpService;
            _choicesHttpService = menuChoicesHttpService;
            _holidayHttpService = holidayHttpService;
            Date = DateTime.Now;
            _userHttpService = userHttpService;
        }

        public byte[] GetImage()
        {
            return Menu != null ? Menu.MenuImage : null;
        }

        public string GetMenuChoice()
        {
            return MenuChoice == null ? "" : MenuChoice.Choice;
        }

        public string GetDescriptionHoliday()
        {
            return DescriptionHoliday != null ? $"per festivit� : {DescriptionHoliday}" : "";
        }

        public async Task ReloadHoliday()
        {
            HttpResponseMessage responseMessage = await _holidayHttpService.GetAll();
            if (responseMessage != null && responseMessage.StatusCode == HttpStatusCode.OK)
            {
                List<Holiday> holidays = await responseMessage.Content.ReadFromJsonAsync<List<Holiday>>();
                if (holidays == null || holidays.Count == 0) return;

                Holidays = holidays;
            }
        }


        public async Task ReloadMenu()
        {
            if (Holidays != null && Holidays.Count != 0)
            {
                Holiday currentHoliday = Holidays.Find(holiday => holiday.Date.Day == Date.Day && holiday.Date.Month == Date.Month);
                if (currentHoliday != null) DescriptionHoliday = currentHoliday.Description;
            }

            HttpResponseMessage responseMessage = await _menuHttpService.GetByDate(Date.Year, Date.Month, Date.Day);

            if (responseMessage != null && responseMessage.StatusCode == HttpStatusCode.OK)
            {
                Menu menu = await responseMessage.Content.ReadFromJsonAsync<Menu>();
                if (menu != null) Menu = menu;

            }

        }



        public async Task<HttpResponseMessage> Add(string choice, int idUser, int idMenu)
        {
            if (choice == null) { throw new Exception("valori non ammessi"); }
            HttpResponseMessage response = await _choicesHttpService.Add(new MenuChoicesDto(idMenu, choice, idUser));
            return response;
        }

        public async Task<HttpResponseMessage> Delete(int idMenu, int idUser)
        {
            HttpResponseMessage response = await _choicesHttpService.Delete(idMenu, idUser);
            return response;
        }
    }
}

