using prenotazione_postazioni_libs.Models;
namespace prenotazioni_postazioni_api.Repositories
{
    public class UtenteRepository
    {

        /// <summary>
        /// Query al db, restituisce un utente mediante il suo id
        /// </summary>
        /// <param name="id">L'id dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal UtenteDto FindById(int id)
        {
            return null;
        }

        /// <summary>
        /// Query al db, restituisce un utente mediante la sua email
        /// </summary>
        /// <param name="email">L'email dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal UtenteDto FindByEmail(string email)
        {
            return null;
        }
    }
}