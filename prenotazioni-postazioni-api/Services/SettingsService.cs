using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class SettingsService
    {
        private SettingsRepository _settingsRepository;
        private readonly ILog logger = LogManager.GetLogger(typeof(SettingsService));

        public SettingsService(SettingsRepository settingsRepository)
        {
            this._settingsRepository = settingsRepository;
        }

        /// <summary>
        /// Restituisce il valore Impostazione Emergenza situato nella tabella Impostazioni nel database.
        /// </summary>
        /// <returns>Valore effettivo dell'impostazione di emergenza. True, o False</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        public bool Get()
        {
            logger.Info("Chiamando Get() per trovare l'impostazione di emergenza...");
            Impostazioni settings = _settingsRepository.Get();
            logger.Info("Controllando se impostazioni (Risultato trovato) e' valida...");
            if (settings == null)
            {
                logger.Warn("Impostazioni non e' valida! Ho lanciato una PrenotazionePostazioniApiException!");
                throw new PrenotazionePostazioniApiException("Impostazione di emergenza non trovata");
            }
            else
            {
                logger.Info("Impostazioni e' valida!");
                return settings.ModEmergenza;

            }
        }

        /// <summary>
        /// Aggiorna il campo di Impostazioni Emergenza nel Database con il valore inserito nel primo parametro
        /// </summary>
        /// <param name="userValue">Il valore con cui si aggiornera Impostazioni Emergenza</param>
        /// <returns>Lo stato di Impostazione Emergenza aggiornata</returns>
        public void Update()
        {
            logger.Info("Controllando se Impostazioni Emergenza e' a true...");
            if (Get() == true)
            {
                logger.Info("Impostazione emergenza e' a true! L'ho cambiato a false!");
                _settingsRepository.Set();
            }
            else
            {
                logger.Info("Impostazione emergenza e' a false! L'ho cambiato a true!");
                _settingsRepository.Set();
            }
        }
        
    }
}
