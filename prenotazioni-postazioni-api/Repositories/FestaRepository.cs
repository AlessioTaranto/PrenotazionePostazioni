using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;

namespace prenotazioni_postazioni_api.Repositories
{
    public class FestaRepository
    {
        private DatabaseManager _databaseManager;
        private readonly ILogger<FestaRepository> logger;

        public FestaRepository(DatabaseManager databaseManager, ILogger<FestaRepository> logger)
        {
            _databaseManager = databaseManager;
            this.logger = logger;
        }



        /// <summary>
        /// query al db, restituisce tutte le festa in una data
        /// </summary>
        /// <param name="date">la data</param>
        /// <returns>Lista di Feste</returns>
        internal Festa FindByDate(DateTime date)
        {
            string query = $"SELECT * FROM Feste WHERE giorno = '{date.ToString("yyyy-MM-dd hh:mm:ss:fff")}';";
            logger.LogInformation("Connessione al database...");
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            logger.LogInformation("Effettuando la query al database...");
            logger.LogInformation("Deserializzando il json restituito da DatabaseManager...");
            Festa festa = JsonConvert.DeserializeObject<Festa>(_databaseManager.GetOneResult(query));
            logger.LogInformation("Disconetto dal database");
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
            logger.LogInformation("Mi connetto al database...");
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            logger.LogInformation("Deserializzo il json returnato da GetAllResults...");
            List<Festa> feste = JsonConvert.DeserializeObject<List<Festa>>(_databaseManager.GetAllResults(query));
            logger.LogInformation("Mi disconetto dal database");
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
            logger.LogInformation("Mi connetto al database...");
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            logger.LogInformation("faccio una query al db");
            _databaseManager.GetNoneResult(query);
            logger.LogInformation("mi disconnetto dal db");
            _databaseManager.DeleteConnection();
        }
    }
}
