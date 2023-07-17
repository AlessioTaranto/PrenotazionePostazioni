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
        public DateOnly Day { get; set; }

        public Menu Menu { get; set; }
        public MenuChoices MenuChoice { get; set; }
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
            if(Menu != null)
            {
            //Menu.Image = "https://marketplace.canva.com/EAD0UMPtOv4/1/0/1236w/canva-oro-tenue-elegante-matrimonio-menu-FQIyCfgp1aY.jpg";
            return Menu.Image;
            }
            return "";
        }
       
        public string GetMenuChoice()
        {
            return MenuChoice == null ? "" : MenuChoice.Choice;
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

            
            HttpResponseMessage responseMessage = await _menuHttpService.GetAll();
            if (responseMessage != null && responseMessage.StatusCode == HttpStatusCode.OK)
            {
                List<Menu> lista = await responseMessage.Content.ReadFromJsonAsync<List<Menu>>();
                if (lista.Count == 0 || lista == null) return;
                foreach (Menu menu in lista)
                {
                    if (menu.Date.Year == Date.Year && menu.Date.Month == Date.Month && menu.Date.Day == Date.Day)
                    {
                        Menu = menu;
                        break;
                    }
                }
            }
        }
       
        public async Task<HttpStatusCode> ExistsChoice(int idMenu, int idUser)
        {
            HttpResponseMessage responseMessage = await _choicesHttpService.GetByUserAndIdMenu(idMenu, idUser);
            //HttpResponseMessage responseMessage = await _choicesHttpService.GetAll();

            
            if (responseMessage != null && responseMessage.StatusCode == HttpStatusCode.OK)
            {

                List<MenuChoices> menuChoices = await responseMessage.Content.ReadFromJsonAsync<List<MenuChoices>>();
                if (menuChoices != null && menuChoices.Count != 0)
                {
                    foreach (MenuChoices choice in menuChoices)
                    {
                        if (choice.IdUser == idUser && choice.IdMenu == idMenu)
                            return HttpStatusCode.OK;
                    }

                }
                return HttpStatusCode.NotFound;
            }
            return HttpStatusCode.UnprocessableEntity;


        }

        public async Task<HttpResponseMessage> Add(string choice, int idUser, int idMenu)
        {
            if(choice == null) { throw new Exception("valori non ammessi"); }
            HttpResponseMessage response = await _choicesHttpService.Add(new MenuChoicesDto(idMenu, choice, idUser));
            return response;
        }

        public async Task<HttpResponseMessage> Delete(int idMenu, int idUser)
        {
            HttpResponseMessage response = await _choicesHttpService.Delete(idMenu,idUser);
            return response;
        }
    }
}

