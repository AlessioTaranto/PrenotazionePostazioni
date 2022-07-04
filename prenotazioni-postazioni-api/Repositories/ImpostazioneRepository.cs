namespace prenotazioni_postazioni_api.Repositories
{
    public class ImpostazioneRepository
    {
        private static ImpostazioneRepository instance;
        private ImpostazioneRepository()
        {
        }
        public static ImpostazioneRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new ImpostazioneRepository();
            }
            return instance;
        }
    }
}
