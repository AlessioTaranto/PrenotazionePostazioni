using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;

namespace prenotazioni_postazioni_api.Repositories
{
    public class RuoloRepository
    {
        private DatabaseManager _databaseManager = new DatabaseManager();
        /// <summary>
        /// Query al db, restituisce il ruolo dell'utente associato usando il suo ID
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Ruolo trovato, null altrimenti</returns>
        public Ruolo FindById(int idRuolo)
        {
            string query = $"SELECT descRuolo, idRuolo, accessoImpostazioni FROM Ruoli WHERE idRuolo = {idRuolo};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            Ruolo ruolo = JsonConvert.DeserializeObject<Ruolo>(_databaseManager.GetOneResult(query));
            _databaseManager.DeleteConnection();
            return ruolo;
        }

        public Ruolo FindByIdUtente(int idUtente)
        {
            string query = $"SELECT idRuolo FROM dbo.Utenti WHERE idUtente = {idUtente};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            int idRuolo = JsonConvert.DeserializeObject<int>(_databaseManager.GetOneResult(query));
            query = $"SELECT* FROM dbo.Ruoli WHERE idRuolo = {idRuolo};";
            Ruolo ruolo = JsonConvert.DeserializeObject<Ruolo>(_databaseManager.GetOneResult(query));
            return ruolo;
        }

        internal void UpdateRuoloUtente(int idRuolo)
        {
            string query = $"UPDATE Ruolo SET accessoImpostazioni 1 ^ accessoImpostazioni WHERE idRuolo = {idRuolo};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            _databaseManager.GetNoneResult(query);
            _databaseManager.DeleteConnection();
        }
    }
}
