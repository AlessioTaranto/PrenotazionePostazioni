using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Repositories.Database;
namespace prenotazioni_postazioni_api.Repositories
{
    public class VotoRepository
    {
        private DatabaseManager _databaseManager = new DatabaseManager();
        /// <summary>
        /// Query al db, restituisce tutti i voti fatti ad un utente
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Lista di voti</returns>
        internal List<Voto> FindAllByIdUtenteFrom(int idUtente)
        {
            return null;
        }
        
        /// <summary>
        /// Query al db, restituisce tutti i voti dell'utente che ha votato
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Lista di voti</returns>
        internal List<Voto> FindAllByIdUtenteTo(int idUtente)
        {
            return null;
        }
        
        internal Voto UpdateVoti(Utente utenteTo, List<Utente> utenteFrom)
        {
            return null;
        }
    }
}
