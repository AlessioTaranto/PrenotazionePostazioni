namespace prenotazioni_postazioni_api.Repositories
{
    public class ImpostazioneRepository
    {

        /// <summary>
        /// Restituisce il valore Impostazione Emergenza situato nella tabella Impostazioni nel database.
        /// </summary>
        /// <returns>Valore effettivo dell'impostazione di emergenza. True, o False</returns>
        public bool FindImpostazioneEmergenza()
        {
            //TODO si bisogna fare una query al db
            return false;
        }


        /// <summary>
        /// Aggiorna il campo di Impostazioni Emergenza nel Database con il valore inserito nel primo parametro
        /// </summary>
        /// <param name="userValue">Il valore con cui si aggiornera Impostazioni Emergenza</param>
        /// <returns>Lo stato di Impostazione Emergenza aggiornata</returns>
        public bool UpdateImpostazioneEmergenza(bool userValue)
        {
            //TODO si bisogna fare una query al db per aggiornare il campo IMPOSTAZIONE EMERGENZA con *userValue*
            return false;
        }
    }
}
