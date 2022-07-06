using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
namespace prenotazioni_postazioni_api.Repositories
{
    public class PrenotazioneRepository
    {
        /// <summary>
        /// Query al db per restitire una Prenotazione in base al suo Id
        /// </summary>
        /// <param name="idPrenotazione">Id della Prenotazione</param>
        /// <returns>Prenotazione</returns>
        internal Prenotazione FindById(int idPrenotazione)
        {
            
            throw new NotImplementedException();
        }
        /// <summary>
        /// Query al db per restituire una Prenotazione in base all'id della stanza associata
        /// </summary>
        /// <param name="idStanza">L'Id della stanza associata alla Prenotazione</param>
        /// <returns>Prenotazione</returns>
        internal Prenotazione FindByStanza(string idStanza)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Query al db per salvare una prenotazione nel database
        /// </summary>
        internal List<Prenotazione> FindAll()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Query al db per restituire una Prenotazione in base all'id dell'utente
        /// </summary>
        /// <param name="idUtente">L'id dell'utente associata alla Prenotazione</param>
        /// <returns>Prenotazione</returns>

        internal Prenotazione FindByUtente(string idUtente)
        {
            throw new NotImplementedException();
        }
    }
}
