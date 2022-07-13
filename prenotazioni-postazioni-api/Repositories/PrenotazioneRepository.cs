using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
namespace prenotazioni_postazioni_api.Repositories
{
    public class PrenotazioneRepository
    {
        private DatabaseManager _databaseManager = new DatabaseManager();
        /// <summary>
        /// Query al db per restitire una Prenotazione in base al suo Id
        /// </summary>
        /// <param name="idPrenotazione">Id della Prenotazione</param>
        /// <returns>Prenotazione</returns>
        internal Prenotazione FindById(int idPrenotazione)
        {

            string query = $"SELECT * FROM Prenotazioni WHERE idPrenotazione = {idPrenotazione};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            Prenotazione prenotazione = JsonConvert.DeserializeObject<Prenotazione>(_databaseManager.GetOneResult(query));
            _databaseManager.DeleteConnection();
            return prenotazione;
        }
        /// <summary>
        /// Query al db per restituire una Prenotazione in base all'id della stanza associata
        /// </summary>
        /// <param name="idStanza">L'Id della stanza associata alla Prenotazione</param>
        /// <returns>Lista di Prenotazione</returns>
        internal List<Prenotazione> FindByStanza(int idStanza)
        {
            string query = $"SELECT * FROM Prenotazioni WHERE idStanza = {idStanza};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            List<Prenotazione> prenotazioni = JsonConvert.DeserializeObject<List<Prenotazione>>(_databaseManager.GetAllResults(query));
            _databaseManager.DeleteConnection();
            return prenotazioni;
        }

        /// <summary>
        /// Query al db, per trovare tutte le prenotazioni esistenti al database
        /// </summary>
        /// <returns>Lista di Prenotazione</returns>
        internal List<Prenotazione> FindAll()
        {
            string query = "SELECT * FROM Prenotazioni;";
            _databaseManager.CreateConnectionToDatabase(null, null, false);
            List<Prenotazione> prenotazioni = JsonConvert.DeserializeObject<List<Prenotazione>>(_databaseManager.GetAllResults(query));
            _databaseManager.DeleteConnection();
            return prenotazioni;
        }
        /// <summary>
        /// Query al db per restituire una Prenotazione in base all'id dell'utente
        /// </summary>
        /// <param name="idUtente">L'id dell'utente associata alla Prenotazione</param>
        /// <returns>Prenotazione</returns>
        internal List<Prenotazione> FindByUtente(int idUtente)
        {
            string query = $"SELECT * FROM Prenotazioni WHERE idUtente = {idUtente};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            List<Prenotazione> prenotazioni = JsonConvert.DeserializeObject<List<Prenotazione>>(_databaseManager.GetAllResults(query));
            _databaseManager.DeleteConnection();
            return prenotazioni;

        }

        /// <summary>
        /// Query al db, aggiunge una nuova prenotazione nella tabella Prenotazioni
        /// </summary>
        /// <param name="prenotazione">La prenotazione da aggiungere al database</param>
        internal void Save(Prenotazione prenotazione)
        {
            string query = $"INSERT INTO Prenotazioni (startDate, endDate, idStanza, idUtente) VALUES ({prenotazione.StartDate.ToString("yyyy-MM-dd hh:mm:ss:fff")}, {prenotazione.EndDate.ToString("yyyy-MM-dd hh:mm:ss:fff")}, {prenotazione.IdStanza}, {prenotazione.IdUtente});";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            _databaseManager.GetNoneResult(query);
            _databaseManager.DeleteConnection();
        }


        /// <summary>
        /// Query al db, restituisce una lista di prenotazione mediante la stanza e l'intervallo di tempo
        /// </summary>
        /// <param name="idStanza">L'id della stanza dove sono effettuate delle prenotazioni</param>
        /// <param name="date">La data delle prenotazioni</param>
        /// <returns></returns>
        internal List<Prenotazione> FindAllByIdStanzaAndDate(int idStanza, DateTime startDate, DateTime endDate)
        {
            string start = startDate.ToString("yyyy-MM-dd hh:mm:ss:fff");
            string end = endDate.ToString("yyyy-MM-dd hh:mm:ss:fff");
            string query = $"SELECT * FROM dbo.Prenotazioni WHERE((idStanza = {idStanza}) AND((DATEDIFF(ss, startDate, {start}) <= 0 AND DATEDIFF(ss, endDate, {start}) >= 0)OR(DATEDIFF(ss, startDate, {end}) <= 0 AND DATEDIFF(ss, endDate, {end}) >= 0)OR(DATEDIFF(ss, startDate, {start}) >= 0 AND DATEDIFF(ss, endDate, {end}) <= 0)))";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            List<Prenotazione> prenotazioni = JsonConvert.DeserializeObject<List<Prenotazione>>(_databaseManager.GetAllResults(query));

            return prenotazioni;
        }





    }
}
