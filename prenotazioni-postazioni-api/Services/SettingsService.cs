using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class SettingsService
    {
        private SettingsRepository _settingsRepository;
        private UserRepository _userRepository;
        private RoleRepository _roleRepository;
        private readonly ILog _logger = LogManager.GetLogger(typeof(SettingsService));

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
            _logger.Info("Chiamando Get() per trovare l'impostazione di emergenza...");
            Settings settings = _settingsRepository.Get();
            _logger.Info("Controllando se impostazioni (Risultato trovato) e' valida...");
            if (settings == null)
            {
                _logger.Warn("Impostazioni non e' valida! Ho lanciato una PrenotazionePostazioniApiException!");
                throw new PrenotazionePostazioniApiException("Impostazione di emergenza non trovata");
            }
            else
            {
                _logger.Info("Impostazioni e' valida!");
                return settings.ModEmergency;

            }
        }

        /// <summary>
        /// Aggiorna il campo di Impostazioni Emergenza nel Database con il valore inserito nel primo parametro
        /// </summary>
        /// <param name="userValue">Il valore con cui si aggiornera Impostazioni Emergenza</param>
        /// <returns>Lo stato di Impostazione Emergenza aggiornata</returns>
        public bool Update(int idAdmin)
        {
            _logger.Info("Trovando un utente admin mediante il suo idL: " + idAdmin);
            User admin = _userRepository.GetById(idAdmin);
            _logger.Info("Controllando se admin e' valido");
            if (admin == null)
            {
                _logger.Error("Admin non e' valido");
                throw new PrenotazionePostazioniApiException("admin e' null");
            }
            _logger.Info("Trovando il ruolo dell'admin...");
            Console.WriteLine("Ruolo admin: " + admin.IdRole);
            Role? roleAdmin = _roleRepository.GetById(admin.IdRole);
            _logger.Info("Controllando se il ruolo admin e' valido...");
            if (roleAdmin == null)
            {
                _logger.Error("Il ruolo dell'admin non e' valido!");
                throw new PrenotazionePostazioniApiException("ruolo admin non trovato");
            }
            _logger.Info("Il ruolo dell'admin è valido!");
            _logger.Info("Procedo con il controllo se admin ha i permessi!");
            if (!roleAdmin.AccessToSettings)
            {
                _logger.Error("Admin non ha i permessi!");
                return false;
            }
            _logger.Info("Admin ha i permessi");
            _logger.Info("Controllando se Impostazioni Emergenza e' a true...");
            if (Get() == true)
            {
                _logger.Info("Impostazione emergenza e' a true! L'ho cambiato a false!");
                _settingsRepository.Set();
            }
            else
            {
                _logger.Info("Impostazione emergenza e' a false! L'ho cambiato a true!");
                _settingsRepository.Set();
            }
            return true;
        }
        
    }
}
