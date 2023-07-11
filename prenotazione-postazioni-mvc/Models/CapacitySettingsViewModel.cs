using System.Diagnostics.Contracts;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_mvc.HttpServices;
using prenotazione_postazioni_libs.Models;

namespace prenotazione_postazioni_mvc.Models
{
    public class CapacitySettingsViewModel
    {

        // room selezionata
        public string? Room { get; set; }

        // Lista capienza normale di tutte le stanze
        public Dictionary<string, int>? Capacity { get; set; }

        // Lista capienza covid di tutte le stanze
        public Dictionary<string, int>? CapacityEmergency { get; set; }

        //Http service
        public CapacityHttpService Service { get; set; }

        public CapacitySettingsViewModel(string? room, bool modEmergency, Dictionary<string, int>? capacity, Dictionary<string, int>? Emergency, CapacityHttpService service)
        {
            Room = room;
            Capacity = capacity;
            CapacityEmergency = Emergency;
            this.Service = service;
        }

        public CapacitySettingsViewModel(string? room, bool modEmergency, CapacityHttpService service)
        {
            Room = room;
            this.Service = service;
            LoadCapacities();
        }

        public CapacitySettingsViewModel(bool modEmergency, CapacityHttpService service)
        {
            Room = "null";
            this.Service = service;
            LoadCapacities();
        }

        public CapacitySettingsViewModel(CapacityHttpService service)
        {
            Room = "null";
            this.Service = service;
            LoadCapacities();
        }


        /// <summary>
        ///     Formatta la visualizzazione della room nella Div a destra
        /// </summary>
        /// <returns>Se è stata selezionata una room, ritorna la room, altrimenti "Seleziona una rooms"</returns>
        public string GetRoom()
        {
            return Room == "null" ? "Seleziona una stanza" : Room;
        }

        /// <summary>
        ///     Carica il Dizionario delle Capienze con valori
        /// </summary>
        private async void LoadCapacities()
        {
            Capacity = new Dictionary<string, int>();
            CapacityEmergency = new Dictionary<string, int>();

            HttpResponseMessage? getAllRooms = await Service.GetAllRooms();
            if (getAllRooms == null || getAllRooms.StatusCode != HttpStatusCode.OK)
                return;

            List<Room>? rooms = await getAllRooms.Content.ReadFromJsonAsync<List<Room>?>();

            foreach (var room in rooms)
            {
                Capacity.Add(room.Name, room.Capacity);
                CapacityEmergency.Add(room.Name, room.CapacityEmergency);
            }
        }

        /// <summary>
        ///     Ottieni la capienza normale di una room 
        /// </summary>
        /// <param name="room">room selezionata</param>
        /// <returns>Capienza Normale della room selezionata</returns>

        public int GetCapacity(string room)
        {

            if (room == null)
                return -1;

            if (!Capacity.ContainsKey(room))
                throw new Exception("room non valida");

            return Capacity[room];
        }

        /// <summary>
        ///     Ottieni la capienza covid di una room 
        /// </summary>
        /// <param name="room">room selezionata</param>
        /// <returns>Capienza Covid della room selezionata</returns>
        
        public int GetCapacityEmergency(string room)
        {
            if (room == null)
                return -1;

            if (!CapacityEmergency.ContainsKey(room))
                throw new Exception("room non valida");

            return CapacityEmergency[room];
        }

        /// <summary>
        ///     Imposta la capienza Normale di una room
        /// </summary>
        /// <param name="room">room selzionata</param>
        /// <param name="capacity">Capienza selezionata</param>

        public async void SetCapacity(string room, int capacity)
        {
            if (capacity <= 0)
                throw new Exception("Capienza non valida");
            if (room == null || !Capacity.ContainsKey(room))
                throw new Exception("room non valida");

            HttpResponseMessage? setCapacity = await Service.SetCapacity(room, capacity);
            if (setCapacity == null || setCapacity.StatusCode != HttpStatusCode.OK)
                return;

            LoadCapacities();
        }

        /// <summary>
        ///     Imposta la capienza Covid di una room
        /// </summary>
        /// <param name="room">room selzionata</param>
        /// <param name="capacity">Capienza selezionata</param>

        public async void SetCapacityEmergency(string room, int capacity)
        {
            if (capacity <= 0)
                throw new Exception("Capienza non valida");
            if (room == null || !CapacityEmergency.ContainsKey(room))
                throw new Exception("room non valida");

            HttpResponseMessage? setCapienza = await Service.SetCapacityEmergency(room, capacity);
            if (setCapienza == null || setCapienza.StatusCode != HttpStatusCode.OK)
                return;

            LoadCapacities();
        }

        /// <summary>
        ///     Funzione utilizzata solo per caricare un numero in caso di fail setCapienza
        /// </summary>
        /// <param name="room">room selezionata</param>
        /// <returns>int</returns>

        public int GetCapacityDefault(string room)
        {
            if (room == null || !Capacity.ContainsKey(room))
                return 0;

            return Capacity[room];
        }

        /// <summary>
        ///     Funzione utilizzata solo per caricare un numero in caso di fail setCapienza
        /// </summary>
        /// <param name="room">room selezionata</param>
        /// <returns>int</returns>

        public int GetCapacityEmergencyDefault(string room)
        {
            if (room == null || !CapacityEmergency.ContainsKey(room))
                return 0;

            return CapacityEmergency[room];
        }

    }
}
