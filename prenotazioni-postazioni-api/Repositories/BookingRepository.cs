using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
using log4net;
using System.Data.SqlClient;

namespace prenotazioni_postazioni_api.Repositories
{
    public class BookingRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(BookingRepository));

        public BookingRepository() { }

        /// <summary>
        /// Query al db per restitire una Prenotazione in base al suo Id
        /// </summary>
        /// <param name="idBooking">Id della Prenotazione</param>
        /// <returns>Prenotazione</returns>
        internal Booking? GetById(int idBooking)
        {
            string query = "SELECT * FROM Booking WHERE id = @idBooking;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idBooking", idBooking);
            return DatabaseManager<Booking>.GetInstance().MakeQueryOneResult(sqlCommand);
        }
        /// <summary>
        /// Query al db per restituire una Prenotazione in base all'id della stanza associata
        /// </summary>
        /// <param name="idRoom">L'Id della stanza associata alla Prenotazione</param>
        /// <returns>Lista di Prenotazione</returns>
        internal List<Booking>? GetByRoom(int idRoom)
        {
            string query = $"SELECT * FROM Booking WHERE idRoom = @idRoom;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@idRoom", idRoom);
            return DatabaseManager<List<Booking>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        /// <summary>
        /// Query al db, per trovare tutte le prenotazioni esistenti al database
        /// </summary>
        /// <returns>Lista di Prenotazione</returns>
        internal List<Booking>? GetAll()
        {
            string query = "SELECT * FROM Booking;";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<Booking>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }
        /// <summary>
        /// Query al db per restituire una Prenotazione in base all'id dell'utente
        /// </summary>
        /// <param name="idUser">L'id dell'utente associata alla Prenotazione</param>
        /// <returns>Prenotazione</returns>
        internal List<Booking>? GetByUser(int idUser)
        {
            string query = $"SELECT * FROM Booking WHERE idUser = @idUser;";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@idUser", idUser);
            return DatabaseManager<List<Booking>>.GetInstance().MakeQueryMoreResults(cmd);

        }

        /// <summary>
        /// Query al db, aggiunge una nuova prenotazione nella tabella Prenotazioni
        /// </summary>
        /// <param name="booking">La prenotazione da aggiungere al database</param>
        internal void Add(Booking booking)
        {
            string query = $"INSERT INTO Booking (startDate, endDate, idRoom, idUser) VALUES (@startDate, @endDate, @idRoom, @idUser);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@startDate", booking.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"));
            sqlCommand.Parameters.AddWithValue("@endDate", booking.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"));
            sqlCommand.Parameters.AddWithValue("@idUser", booking.IdUser);
            sqlCommand.Parameters.AddWithValue("@idRoom", booking.IdRoom);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }


        /// <summary>
        /// Query al db, restituisce una lista di prenotazione mediante la stanza e l'intervallo di tempo
        /// </summary>
        /// <param name="idRoom">L'id della stanza dove sono effettuate delle prenotazioni</param>
        /// <param name="date">La data delle prenotazioni</param>
        /// <returns></returns>
        internal List<Booking>? GetAllByRoomDate(int idRoom, DateTime startDate, DateTime endDate)
        {
            string start = startDate.ToString("yyyy-MM-ddTHH:mm:ss");
            string end = endDate.ToString("yyyy-MM-ddTHH:mm:ss");
            string query = $"SELECT * FROM Booking WHERE((idRoom = @idRoom)AND((startDate BETWEEN @start AND @end) OR(endDate BETWEEN @start AND @end) OR (DATEDIFF(HH, startDate, @start) >= 0 AND DATEDIFF(HH, endDate, @end) <= 0)))";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_stanza", idRoom);
            sqlCommand.Parameters.AddWithValue("@start", start);
            sqlCommand.Parameters.AddWithValue("@end", end);
            return DatabaseManager<List<Booking>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        /// <summary>
        /// Cancella una prenotazione dal DB
        /// </summary>
        /// <param name="idBooking">Id della prenotazione da cancellare</param>
        internal void Delete(int idBooking)
        {
            string query = $"DELETE FROM Booking WHERE id = @idBooking   ;";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@idBooking", idBooking);
            DatabaseManager<List<Booking>>.GetInstance().MakeQueryMoreResults(cmd);
        }





    }
}
