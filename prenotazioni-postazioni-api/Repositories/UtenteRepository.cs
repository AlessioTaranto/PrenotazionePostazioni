using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
namespace prenotazioni_postazioni_api.Repositories
{
    public class UtenteRepository
    {
        private DatabaseManager _databaseManager = new DatabaseManager();
        /// <summary>
        /// Query al db, restituisce un utente mediante il suo id
        /// </summary>
        /// <param name="id">L'id dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal Utente FindById(int idUtente)
        {
            string query = $"SELECT nome, cognome, idUtente, immagine, email, idRuolo FROM Utenti WHERE idUtente = {idUtente};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            Utente utente = (Utente)JsonConvert.DeserializeObject(_databaseManager.GetOneResult(query));
            _databaseManager.DeleteConnection();
            return utente;
        }

        /// <summary>
        /// Query al db, restituisce un utente mediante la sua email
        /// </summary>
        /// <param name="email">L'email dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal Utente FindByEmail(string email)
        {
            string query = $"SELECT idUtente, nome, cognome, email, immagine, idRuolo FROM Utenti WHERE email = {email};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            Utente utente = (Utente)JsonConvert.DeserializeObject(_databaseManager.GetOneResult(query));
            _databaseManager.DeleteConnection();
            return utente;
        }
    }
}