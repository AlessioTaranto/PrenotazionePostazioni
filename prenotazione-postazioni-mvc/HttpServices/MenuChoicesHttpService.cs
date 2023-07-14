using Newtonsoft.Json;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using System.Text;
namespace prenotazione_postazioni_mvc.HttpServices
{
    public class MenuChoicesHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public MenuChoicesHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-MenuChoices");
            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/menuChoices/getAll");
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-MenuChoices");
            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/menuChoices/getById?id={id}");
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByIdMenu(int idMenu)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-MenuChoices");
            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/menuChoices/getByIdMenu?idMenu={idMenu}");
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetByUserAndIdMenu(int idMenu, int idUser)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazioni-MenuChoices");
            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/menuChoices/getByUserAndIdMenu?idMenu={idMenu}&idUser={idUser}");
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> Add(MenuChoicesDto menuChoicesDto)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-MenuChoices");
            string json = JsonConvert.SerializeObject(menuChoicesDto);
            //string json = "{" + "\"Id menu\":\"" + menuChoicesDto.IdMenu + "\", " + "\"scelta\": \"" + menuChoicesDto.Choice + "\", " + "\"id utente\": \"" + menuChoicesDto.IdUser + "\"}" + "";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //Console.WriteLine(JsonConvert.SerializeObject(menuChoicesDto));
            //Console.WriteLine("\\n content : " + json);
            var httpResponseMessage =
               await httpClient.PostAsync($"https://localhost:7126/api/menuChoices/add", content);
           
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> Delete(int idMenu, int idUser)
        {
            var httpClient = _httpClientFactory.CreateClient("PrenotazionePostazione-MenuChoices");

            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7126/api/menuChoices/delete?idMenu={idMenu}&idUser={idUser}");

            return httpResponseMessage;
        }


    }
}
