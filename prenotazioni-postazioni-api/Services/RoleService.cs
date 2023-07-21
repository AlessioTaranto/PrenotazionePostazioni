using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class RoleService
    {
        private RoleRepository _roleRepository;
        private UserRepository _userRepository;
        private readonly ILog _logger = LogManager.GetLogger(typeof(RoleService));

        public RoleService(RoleRepository roleRepository, UserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public List<Role> GetAll()
        {
            _logger.Info("Trovando tutti i ruoli nel database...");
            return _roleRepository.GetAll();
        }

        /// <summary>
        /// Restituisce il Ruolo associato all'utente mediante il suo id
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Ruolo trovato, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        public Role GetById(int idUser)
        {
            _logger.Info("Trovando il ruolo mediante il suo id: " + idUser);
            Role? role = _roleRepository.GetById(idUser);
            if (role == null)
            {
                _logger.Warn("Ruolo non trovato! Lancio una PrenotazionePrenotazioniApiException!");
                throw new PrenotazionePostazioniApiException("IdRole utente non trovato");
            }
            else
            {
                _logger.Info("Ruolo trovato!");
                return role;

            }
        }

            /// <summary>
            /// Serve a ricercare un Ruolo di un utente tramite l'id dell'utente
            /// </summary>
            /// <param name="idUser"></param>
            /// <returns></returns>
            /// <exception cref="PrenotazionePostazioniApiException"></exception>
        public Role GetByIdUser(int idUser)
        {
            _logger.Info("Trovando un utente mediante il suo id utente: " + idUser);
            Role? role = _roleRepository.GetByUser(idUser);
            _logger.Info("Controllando se l'utente e' valido...");
            if (role == null)
            {
                _logger.Warn("Utente non trovato, ora lancio una PrenotazionePostazioniApiExcetion!");
                throw new PrenotazionePostazioniApiException("IdRuolo utente non trovato");
            }
            else
            {
                _logger.Info("Utente trovato!");
                return role;
            }
        }

        /// <summary>
        /// switch il ruolo dell'utente, solo se l'admin ha AccessoImpostazioni a TRUE
        /// </summary>
        /// <param name="idUser">L'id del'utente che gli verra modificato l'accesso impostazioni</param>
        /// <param name="idAdmin">L'id dell'admin che effettua il cambiamento di accesso impostazioni all'utente</param>
        /// <returns>TRUE se l'admin ha accesso impostazioni a true, FALSE altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException">Se admin, utente, ruoloUtente, o ruoloAdmin sono null</exception>
        internal bool Update(int idUser, string futureRole)
        {
            _logger.Info("Trovando un utente mediante il suo id: " + idUser);
            User user = _userRepository.GetById(idUser);
            _logger.Info("Controllando se admin e' valido");
            if (user == null)
            {
                _logger.Error("Utente non e' valido");
                throw new PrenotazionePostazioniApiException("utente e' null");
            }
            _logger.Info("Admin e Utente validi!");
            _logger.Info("Trovando il ruolo dell'utente...");
            Role? roleUser = _roleRepository.GetById(user.IdRole);
            if (roleUser == null)
            {
                _logger.Error("Il ruolo dell'utente non e' valido");
                throw new PrenotazionePostazioniApiException("ruolo utente non trovato");
            }
            Role? roleFuture = _roleRepository.GetByName(futureRole);
            if (roleFuture == null)
            {
                _logger.Error("Il ruolo futuro non � valido");
                throw new PrenotazionePostazioniApiException("Ruolo futuro non trovato");
            }
            _logger.Info("Il ruolo dell'admin e dell'utente sono validi!");
            _logger.Info("Admin ha i permessi");
            _logger.Info("Chiedo di cambiare il ruolo dell'utente");
            _roleRepository.UpdateUserRole (user.Id, roleFuture.Id);
            _logger.Info("Ho cambiato il ruolo dell'utente con successo!");
            return true;
        }
    }
}
