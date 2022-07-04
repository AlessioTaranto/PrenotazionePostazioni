using prenotazioni_postazioni_api.Repositories;

namespace prenotazioni_postazioni_api.Services
{
    public class ImpostazioneService
    {
        private ImpostazioneRepository impostazioneRepository = new ImpostazioneRepository();

        public bool GetImpostazioneEmergenza()
        {
            return impostazioneRepository.FindImpostazioneEmergenza();
        }

        public bool ChangeImpostazioniEmergenza(bool userValue)
        {
            return impostazioneRepository.UpdateImpostazioneEmergenza(userValue);
        }
    }
}
