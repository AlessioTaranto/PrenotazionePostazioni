using Newtonsoft.Json;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_mvc.HttpServices;
using System.Net;
using System.Runtime.InteropServices;

namespace prenotazione_postazioni_mvc.Models
{
    public class MenuViewModel
    {
        public DateTime Date { get; set; }
        public DateOnly Day { get; set; }

        public Menu Menu { get; set; }
        public MenuChoices MenuChoices { get; set; }

        public readonly MenuHttpService _menuHttpService;
        public readonly MenuChoicesHttpService _choicesHttpService;



        public MenuViewModel(MenuHttpService menuHttpService, MenuChoicesHttpService menuChoicesHttpService)
        {
            _menuHttpService = menuHttpService;
            _choicesHttpService = menuChoicesHttpService;
            Date = DateTime.Now;
            Menu = new Menu();

        }


        //public string GetDate() { return Date.ToString("dd/MM"); }


        public async Task ReLoadMenu()
        {
            HttpResponseMessage responseMessage = await _menuHttpService.GetByDate(Date.Year, Date.Month, Date.Day);
            if (responseMessage != null && responseMessage.StatusCode == HttpStatusCode.OK)
            {
                Menu menu = await responseMessage.Content.ReadFromJsonAsync<Menu>();
                if (menu != null) Menu = menu;
            }
        }

        public async Task<HttpStatusCode> ExistsChoice(string choice)
        {
            if (choice == null)
                return HttpStatusCode.UnprocessableEntity;

            MenuChoices menuChoice = JsonConvert.DeserializeObject<MenuChoices>(choice);
            if (menuChoice == null)
                return HttpStatusCode.UnprocessableEntity;

            HttpResponseMessage msg = await _choicesHttpService.GetById(menuChoice.Id);

            if (msg != null && msg.StatusCode == HttpStatusCode.OK)
            {

                MenuChoices responseChoice = await msg.Content.ReadFromJsonAsync<MenuChoices>();

                if (responseChoice.Id == menuChoice.Id)
                    return HttpStatusCode.OK;

                return HttpStatusCode.NotFound;

            }

            return HttpStatusCode.UnprocessableEntity;

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
            if(choice == null || idUser < 0 || idMenu < 0)
            {
                throw new Exception("valori non ammessi");
            }
            HttpResponseMessage response = await _choicesHttpService.Add(new MenuChoicesDto(idMenu,choice,idUser));
            if (response == null || response.StatusCode != HttpStatusCode.OK) return;
        }
    }
}

