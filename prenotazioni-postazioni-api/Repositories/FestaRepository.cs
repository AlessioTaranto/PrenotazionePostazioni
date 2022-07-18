using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;

namespace prenotazioni_postazioni_api.Repositories
{
    public class FestaRepository
    {
        private DatabaseManager _databaseManager = new DatabaseManager();

        /// <summary>
        /// query al db, restituisce tutte le festa in una data
        /// </summary>
        /// <param name="date">la data</param>
        /// <returns>Lista di Feste</returns>
        internal Festa FindByDate(DateTime date)
        {
            string query = $"SELECT * FROM Feste WHERE giorno = '{date.ToString("yyyy-MM-dd hh:mm:ss:fff")}';";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            string jsonTest = _databaseManager.GetOneResult(query);
            Console.WriteLine(jsonTest);
            Festa festa = JsonConvert.DeserializeObject<Festa>(jsonTest);
            _databaseManager.DeleteConnection();
            return festa;
        }
        /// <summary>
        /// query al db, restituisce tutte le festa
        /// </summary>
        /// <returns>Lista di festa</returns>
        internal List<Festa> FindAll()
        {
            string query = $"SELECT * FROM Feste";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            List<Festa> feste = JsonConvert.DeserializeObject<List<Festa>>(_databaseManager.GetAllResults(query));
            _databaseManager.DeleteConnection();
            return feste;
        }

        /// <summary>
        /// query al db, salva una festa al database
        /// </summary>
        /// <param name="festa">la festa da salvare</param>
        internal void Save(Festa festa)
        {
            string query = $"INSERT INTO Festa (giorno, descrizione) VALUES ('{festa.Date.ToString("yyyy-MM-dd hh:mm:ss:fff")}', '{festa.Desc})';";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            _databaseManager.GetNoneResult(query);
            _databaseManager.DeleteConnection();
        }
    }
}
