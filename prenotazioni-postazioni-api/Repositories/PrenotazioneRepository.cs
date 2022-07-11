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

            string query = $"SELECT data, idStanza, idPrenotazione, idUtente FROM Prenotazioni WHERE idPrenotazione = {idPrenotazione};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            Prenotazione prenotazione = (Prenotazione)JsonConvert.DeserializeObject(_databaseManager.GetOneResult(query));
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
            string query = $"SELECT idPrenotazioni, idStanza, data, idStanza, idUtente FROM Prenotazioni WHERE idStanza = {idStanza};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            List<Prenotazione> prenotazioni = (List<Prenotazione>) JsonConvert.DeserializeObject(_databaseManager.GetAllResults(query));
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
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            List<Prenotazione> prenotazioni = (List<Prenotazione>) JsonConvert.DeserializeObject(_databaseManager.GetAllResults(query));
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
            string query = $"SELECT idPrenotazioni, data, idUtente, idStanza, idUtente FROM Prenotazioni WHERE idUtente = {idUtente};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            List<Prenotazione> prenotazioni = (List<Prenotazione>)JsonConvert.DeserializeObject(_databaseManager.GetAllResults(query));
            _databaseManager.DeleteConnection();
            return prenotazioni;

        }

        /// <summary>
        /// Query al db, aggiunge una nuova prenotazione nella tabella Prenotazioni
        /// </summary>
        /// <param name="prenotazione">La prenotazione da aggiungere al database</param>
        internal void Save(Prenotazione prenotazione)
        {
            string query = $"INSERT INTO Prenotazioni (data, idStanza, idUtente) VALUES ({prenotazione.Date}, {prenotazione.IdStanza}, {prenotazione.IdUtente});";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            _databaseManager.GetNoneResult(query);
            _databaseManager.DeleteConnection();
        }


        /// <summary>
        /// Query al db, restituisce una lista di prenotazione mediante la stanza e il giorno
        /// </summary>
        /// <param name="idStanza">L'id della stanza dove sono effettuate delle prenotazioni</param>
        /// <param name="date">La data delle prenotazioni</param>
        /// <returns></returns>
        internal List<Prenotazione> FindAllByIdStanzaAndDate(int idStanza, DateTime dateDay)
        {
            return null;
        }





    }
}
