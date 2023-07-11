using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
using log4net;
using System.Data.SqlClient;

namespace prenotazioni_postazioni_api.Repositories
{
    public class RoomRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(RoomRepository));

        public RoomRepository()
        {
        }


        /// <summary>
        /// Query al db, restituisce tutte le stanze presente nel database
        /// </summary>
        /// <returns>Lista di Stanza</returns>
        internal List<Room>? GetAll()
        {
            string query = $"SELECT * FROM Room";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<Room>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }


        /// <summary>
        /// Query al db, restituisce una stanza mediante il suo id
        /// </summary>
        /// <param name="id">L'id della stanza</param>
        /// <returns>La stanza trovata, null altrimenti</returns>
        internal Room? GetById(int idRoom)
        {
            string query = $"SELECT * FROM Room WHERE id = @id_stanza;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_stanza", idRoom);
            return DatabaseManager<Room>.GetInstance().MakeQueryOneResult(sqlCommand);
        }

        /// <summary>
        /// Query al db, restituisce la stanza mediante il suo nome
        /// </summary>
        /// <param name="roomName">Il nome della stanza da trovare</param>
        /// <returns>La stanza trovata, null altrimenti</returns>
        internal Room? GetByName(string roomName)
        {
            string query = $"SELECT * FROM Room WHERE name = @roomName;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@stanza_name", roomName);
            return DatabaseManager<Room>.GetInstance().MakeQueryOneResult(sqlCommand);
        }
        /// <summary>
        /// Query al db, aggiunge una nuova stanza alla tabella Stanze
        /// </summary>
        /// <param name="room">La stanza da aggiungere al db</param>
        internal void Add(Room room)
        {
            string query = $"INSERT INTO Room (name, capacity, capacityEmergency) VALUES (@roomName, @capacity, @capacityEmergency);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@roomName", room.Name);
            sqlCommand.Parameters.AddWithValue("@capacity", room.Capacity);
            sqlCommand.Parameters.AddWithValue("@capacityEmergency", room.CapacityEmergency);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }

        internal void SetCapacity(int capacity, int idRoom)
        {
            string query = "UPDATE Room SET capacity = @capacity WHERE id = @idRoom;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@capacity", capacity);
            sqlCommand.Parameters.AddWithValue("@idRoom", idRoom);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }

        internal void SetCapacityEmergency(int capacityEmergency, int idRoom)
        {
            string query = "UPDATE Room SET capacityEmergency = @capacityEmergency WHERE id = @idRoom;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@capacityEmrgency", capacityEmergency);
            sqlCommand.Parameters.AddWithValue("@idRoom", idRoom);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }
    }
}
