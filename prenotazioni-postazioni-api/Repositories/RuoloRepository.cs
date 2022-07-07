using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
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
        public Ruolo FindByUtenteId(int idUtente)
        {
            
            return null;
        }
    }
}
