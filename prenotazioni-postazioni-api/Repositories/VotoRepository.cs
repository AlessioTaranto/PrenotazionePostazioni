using prenotazione_postazioni_libs.Dto;
using Newtonsoft.Json;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Repositories.Database;

namespace prenotazioni_postazioni_api.Repositories
{
    public class VotoRepository
    {
        private DatabaseManager _databaseManager = new DatabaseManager();

        /// <summary>
        /// Query al db, restituisce tutti i voti dell'utente che ha votato
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Lista di voti</returns>
        internal List<Voto> FindAllByIdUtenteFrom(int idUtente)
        {
            string query = $"SELECT * FROM Voti WHERE idUtente = {idUtente};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            List<Voto> voti = JsonConvert.DeserializeObject<List<Voto>>(_databaseManager.GetAllResults(query));
            _databaseManager.DeleteConnection();
            return voti;
        }

        /// <summary>
        /// Query al db, restituisce tutti i voti fatti ad un utente
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Lista di voti</returns>
        internal List<Voto> FindAllByIdUtenteTo(int idUtente)
        {
            string query = $"SELECT * FROM voti WHERE idUtenteVotato = {idUtente};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            List<Voto> voti = JsonConvert.DeserializeObject<List<Voto>>(_databaseManager.GetAllResults(query));
            _databaseManager.DeleteConnection();
            return voti;
        }

        /// <summary>
        /// query al db, salva un voto al database
        /// </summary>
        /// <param name="voto">il voto che verra salvato nel database</param>
        internal void Save(Voto voto)
        {
            string query = $"INSERT INTO Voti (idUtente, idUtenteVotato, voto) VALUES ({voto.Utente.IdUtente}, {voto.UtenteVotato.IdUtente}, {voto.VotoEffettuato});";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            _databaseManager.GetNoneResult(query);
            _databaseManager.DeleteConnection();
        }
        /// <summary>
        /// query al db, restituisce il voto che un utente ha effettuato ad un altro utente
        /// </summary>
        /// <param name="idUtente">L'utente che ha votato</param>
        /// <param name="idUtenteVotato">L'utente che e' stato votato</param>
        /// <returns>Il voto che trovato, null altrimenti</returns>
        internal Voto? FindByIdUtenteToAndIdUtenteFrom(int idUtente, int idUtenteVotato)
        {
            string query = $"SELECT * FROM Voti WHERE idUtente = {idUtente} AND idUtenteVotato = {idUtenteVotato};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            Voto voto = JsonConvert.DeserializeObject<Voto>(_databaseManager.GetOneResult(query));
            _databaseManager.DeleteConnection();
            return voto;
        }

        /// <summary>
        /// query al db, aggiorna il voto al suo opposto
        /// </summary>
        /// <param name="voto">Il voto da aggiornare</param>
        internal void UpdateVoto(Voto voto)
        {
            string query = $"UPDATE Voti SET voto = 1 ^ voto WHERE idUtente = {voto.Utente.IdUtente} AND idUtenteVotato = {voto.UtenteVotato.IdUtente};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            _databaseManager.GetNoneResult(query);
            _databaseManager.DeleteConnection();
        }
    }
}
