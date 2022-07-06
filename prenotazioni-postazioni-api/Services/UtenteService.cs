using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;

namespace prenotazioni_postazioni_api.Services
{
    public class UtenteService
    {
        UtenteRepository utenteRepository = new UtenteRepository();

        /// <summary>
        /// Resituisce l'utente mediante il suo id
        /// </summary>
        /// <param name="id">L'id dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal Utente GetUtenteById(int id)
        {
            return utenteRepository.FindById(id);
        }

        /// <summary>
        /// Restituisce l'utente mediante la sua email
        /// </summary>
        /// <param name="email">L'email dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal Utente GetUtenteByEmail(string email)
        {
            return utenteRepository.FindByEmail(email);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="utenteDto"></param>
        internal void Save(UtenteDto utenteDto)
        {
            //TODO da fare
        }
    }
}
