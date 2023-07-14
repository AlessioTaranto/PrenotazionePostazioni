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
        internal Holiday? GetByDate(DateTime date)
        {
            string query = "SELECT * FROM Holiday WHERE date = @date;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
            return DatabaseManager<Holiday>.GetInstance().MakeQueryOneResult(sqlCommand);
        }
        /// <summary>
        /// query al db, restituisce tutte le festa
        /// </summary>
        /// <returns>Lista di festa</returns>
        internal List<Holiday>? GetAll()
        {
            string query = $"SELECT * FROM Holiday";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<Holiday>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        /// <summary>
        /// query al db, salva una festa al database
        /// </summary>
        /// <param name="festa">la festa da salvare</param>
        internal void Add(Holiday festa)
        {
            string query = $"INSERT INTO Holiday (date, description) VALUES (@date, @description);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@date", festa.Date.ToString("yyyy-MM-dd"));
            sqlCommand.Parameters.AddWithValue("@description", festa.Description);
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
