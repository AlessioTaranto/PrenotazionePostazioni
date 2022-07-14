using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;

namespace prenotazioni_postazioni_api.Repositories
{
    public class FestaRepository
    {
        private DatabaseManager _databaseManager = new DatabaseManager();

        /// <summary>
        /// query al db, restituisce tutte le feste in una data
        /// </summary>
        /// <param name="date">la data</param>
        /// <returns>Lista di Feste</returns>
        internal List<Festa> FindByDate(DateOnly date)
        {
            string query = $"SELECT * FROM Feste WHERE giorno = {date.ToString("yyyy-MM-dd hh:mm:ss:fff")};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            List<Festa> feste = JsonConvert.DeserializeObject<List<Festa>>(_databaseManager.GetAllResults(query));
            _databaseManager.DeleteConnection();
            return feste;
        }
        /// <summary>
        /// query al db, restituisce tutte le feste
        /// </summary>
        /// <returns>Lista di feste</returns>
        internal List<Festa> FindAll()
        {
            string query = $"SELECT * FROM Feste";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            List<Festa> feste = JsonConvert.DeserializeObject<List<Festa>>(_databaseManager.GetAllResults(query));
            _databaseManager.DeleteConnection();
            return feste;
        }
    }
}
