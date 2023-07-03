using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
using prenotazioni_postazioni_api.Services;
using log4net;
using System.Data.SqlClient;

namespace prenotazioni_postazioni_api.Repositories
{
    public class HolidayRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(HolidayRepository));




        /// <summary>
        /// query al db, restituisce tutte le festa in una data
        /// </summary>
        /// <param name="date">la data</param>
        /// <returns>Lista di Feste</returns>
        internal Festa? GetByDate(DateTime date)
        {
            string query = "SELECT * FROM Holiday WHERE day = @dya;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@day", date.ToString("yyyy-MM-dd"));
            return DatabaseManager<Festa>.GetInstance().MakeQueryOneResult(sqlCommand);
        }
        /// <summary>
        /// query al db, restituisce tutte le festa
        /// </summary>
        /// <returns>Lista di festa</returns>
        internal List<Festa>? GetAll()
        {
            string query = $"SELECT * FROM Holiday";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<Festa>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        /// <summary>
        /// query al db, salva una festa al database
        /// </summary>
        /// <param name="festa">la festa da salvare</param>
        internal void Add(Festa festa)
        {
            string query = $"INSERT INTO Holiday (day, description) VALUES (@day, @description);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@day", festa.Giorno.ToString("yyyy-MM-dd"));
            sqlCommand.Parameters.AddWithValue("@description", festa.Descrizione);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }

        /// <summary>
        /// Rimuove una festività dal database
        /// </summary>
        /// <param name="day">indica il giorno in cui cade la festività</param>
        internal void Delete(DateTime day)
        {
            string query = $"DELETE FROM Holiday WHERE day = @day;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@day", day.ToString("yyyy-MM-dd"));
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }
    }
}
