using Newtonsoft.Json;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace prenotazione_postazioni_mvc.Models
{
    public class MenuViewModel
    {
        public DateTime Date { get; set; }
        public DateOnly Day { get; set; }

        public Menu Menu { get; set; }
        public MenuChoices MenuChoices { get; set; }
        public List<Holiday> Holidays { get; set; }

        public readonly MenuHttpService _menuHttpService;
        public readonly MenuChoicesHttpService _choicesHttpService;
        public readonly HolidayHttpService _holidayHttpService;
        public readonly UserHttpService _userHttpService;


        public MenuViewModel(MenuHttpService menuHttpService, MenuChoicesHttpService menuChoicesHttpService, HolidayHttpService holidayHttpService , UserHttpService userHttpService)
        {
            _menuHttpService = menuHttpService;
            _choicesHttpService = menuChoicesHttpService;
            _holidayHttpService = holidayHttpService;
            Date = DateTime.Now;
            _userHttpService = userHttpService;
        }

        public string GetImage()
        {
            return Menu == null ? "" : Menu.Image;
        }
        public string GetMenuChoice()
        {
            return MenuChoices == null ? "" : MenuChoices.Choice;
        }

        public async Task ReloadHoliday()
        {
            HttpResponseMessage responseMessage = await _holidayHttpService.GetAll();
            if (responseMessage != null && responseMessage.StatusCode == HttpStatusCode.OK)
            {
                List<Holiday> holidays = await responseMessage.Content.ReadFromJsonAsync<List<Holiday>>();
                if (holidays == null || holidays.Count == 0) return;

                Console.WriteLine(holidays);
                Holidays = holidays;
            }
        }


        public async Task ReloadMenu()
        {
            if (Holidays != null)
            {
                foreach (Holiday holiday in Holidays)
                {
                    if (holiday.Date == Date)
                    {
                        return;
                    }
                }
            }

            HttpResponseMessage responseMessage = await _menuHttpService.GetByDate(Date.Year, Date.Month, Date.Day);
            Console.WriteLine(responseMessage.StatusCode.ToString());
            if (responseMessage != null && responseMessage.StatusCode == HttpStatusCode.OK)
            {
                Menu menu = await responseMessage.Content.ReadFromJsonAsync<Menu>();
                if (menu != null) Menu = menu;
            }
        }
       
        public async Task<HttpStatusCode> ExistsChoice(int idMenu, int idUser)
        {
            HttpResponseMessage responseMessage = await _choicesHttpService.GetByUserAndIdMenu(idMenu, idUser);
            if (responseMessage != null && responseMessage.StatusCode == HttpStatusCode.OK)
            {
                Menu menu = await responseMessage.Content.ReadFromJsonAsync<Menu>();
                if (menu != null)
                {

                    return HttpStatusCode.OK;
                }
                else
                {
                    return HttpStatusCode.NotFound;

                }
            }
            return HttpStatusCode.UnprocessableEntity;


        }

        public async void Add(string choice, int idUser, int idMenu)
        {
            if (choice == null || idUser < 0 || idMenu < 0)
            {
                throw new Exception("valori non ammessi");
            }
            HttpResponseMessage response = await _choicesHttpService.Add(new MenuChoicesDto(idMenu, choice, idUser));
            if (response == null || response.StatusCode != HttpStatusCode.OK) return;
        }

        public async void Delete(int idMenu, int idUser)
        {
            if (idMenu < 0 || idUser < 0) throw new Exception("valori non ammessi");
            HttpResponseMessage response = await _choicesHttpService.Delete(idMenu,idUser);
            if(response == null  || response.StatusCode != HttpStatusCode.OK) { return; }
        }
    }
}

