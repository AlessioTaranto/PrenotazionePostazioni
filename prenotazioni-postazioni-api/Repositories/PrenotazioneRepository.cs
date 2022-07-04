namespace prenotazioni_postazioni_api.Repositories
{
    public class PrenotazioneRepository
    {
        private static PrenotazioneRepository istance;

        private PrenotazioneRepository()
        {
        }

        public static PrenotazioneRepository getIstance()
        {
            if(istance == null)
            {
                istance = new PrenotazioneRepository();
            }
            return istance;
        }


    }
}
