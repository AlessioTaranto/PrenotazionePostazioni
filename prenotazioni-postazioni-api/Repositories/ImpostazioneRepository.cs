using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Repositories.Database;
namespace prenotazioni_postazioni_api.Repositories
{
    public class ImpostazioneRepository
    {
        private DatabaseManager _databaseManager = new DatabaseManager();

        /// <summary>
        /// Query al db per restituire il campo Impostazione Emergenza
        /// </summary>
        /// <returns>Lo stato dell'Impostazione Emergenza</returns>
        public bool FindImpostazioneEmergenza()
        {
            
            return false;
        }

        /// <summary>
        /// Query al db per aggiornare lo stato di Impostazione Emergenza
        /// </summary>
        /// <param name="userValue">valore aggiornato, sostituendo lo stato di Impostazione Emergenza vecchia presente nel Database</param>
        /// <returns>Lo stato di Impostazione Emergenza nuova</returns>
        public bool UpdateImpostazioneEmergenza(bool userValue)
        {
            //TODO si bisogna fare una query al db per aggiornare il campo IMPOSTAZIONE EMERGENZA con *userValue*
            return false;
        }
    }
}
