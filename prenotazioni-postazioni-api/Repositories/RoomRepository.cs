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
        internal List<Stanza>? FindAll()
        {
            string query = $"SELECT * FROM Room";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<Stanza>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }


        /// <summary>
        /// Query al db, restituisce una stanza mediante il suo id
        /// </summary>
        /// <param name="id">L'id della stanza</param>
        /// <returns>La stanza trovata, null altrimenti</returns>
        internal Stanza? FindById(int idStanza)
        {
            string query = $"SELECT * FROM Room WHERE id = @id_stanza;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_stanza", idStanza);
            return DatabaseManager<Stanza>.GetInstance().MakeQueryOneResult(sqlCommand);
        }

        /// <summary>
        /// Query al db, restituisce la stanza mediante il suo nome
        /// </summary>
        /// <param name="stanzaName">Il nome della stanza da trovare</param>
        /// <returns>La stanza trovata, null altrimenti</returns>
        internal Stanza? FindByName(string stanzaName)
        {
            string query = $"SELECT * FROM Room WHERE UPPER(Room.name) = UPPER(@stanza_name);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@stanza_name", stanzaName);
            return DatabaseManager<Stanza>.GetInstance().MakeQueryOneResult(sqlCommand);
        }
        /// <summary>
        /// Query al db, aggiunge una nuova stanza alla tabella Stanze
        /// </summary>
        /// <param name="stanza">La stanza da aggiungere al db</param>
        internal void Save(Stanza stanza)
        {
            string query = $"INSERT INTO Room (name, capacity, capacityEmergency) VALUES (@nome_stanza, @stanza_posti_max, @stanza_posti_max_emergenza);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@nome_stanza", stanza.Nome);
            sqlCommand.Parameters.AddWithValue("@stanza_posti_max", stanza.PostiMax);
            sqlCommand.Parameters.AddWithValue("@stanza_posti_max_emergenza", stanza.PostiMaxEmergenza);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }

        internal void ChangePostiMax(int postiMax, int id)
        {
            string query = "UPDATE Room SET capacity = @postiMax WHERE id = @idStanza;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@postiMax", postiMax);
            sqlCommand.Parameters.AddWithValue("@idStanza", id);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }

        internal void ChangePostiMaxEmergenza(int postiMaxEmergenza, int id)
        {
            string query = "UPDATE Room SET capacityEmergency = @postiMax WHERE id = @idStanza;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@postiMax", postiMaxEmergenza);
            sqlCommand.Parameters.AddWithValue("@idStanza", id);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }
    }
}
