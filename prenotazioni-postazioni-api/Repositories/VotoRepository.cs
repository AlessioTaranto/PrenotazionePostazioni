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
            string query = $"SELECT idVoti, idUtente, idUtenteVotato, voto FROM Voti WHERE idUtente = {idUtente};";
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
            string query = $"SELECT idVoti, idUtente, idUtenteVotato, voto FROM voti WHERE idUtenteVotato = {idUtente};";
            _databaseManager.CreateConnectionToDatabase(null, null, true);
            List<Voto> voti = JsonConvert.DeserializeObject<List<Voto>>(_databaseManager.GetAllResults(query));
            _databaseManager.DeleteConnection();
            return voti;
        }
        
        //internal Voto UpdateVoti(Voto voto)
        //{
        //    string query = $"SELECT voto FROM Voti WHERE idVoti = {voto.IdVoto};";
        //    _databaseManager.CreateConnectionToDatabase(null, null, true);
        //    bool votoBool = (bool)JsonConvert.DeserializeObject(_databaseManager.GetOneResult(query));

        //}
    }
}
