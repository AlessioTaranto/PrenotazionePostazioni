using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using prenotazioni_postazioni_api.Utilities;

namespace prenotazioni_postazioni_api.Services
{
    public class RuoloService
    {
        private RuoloRepository _ruoloRepository;
        private UtenteRepository _utenteRepository;
        private readonly ILogger<RuoloService> _logger = Log4NetManager<RuoloService>.GetLogger();

        public RuoloService(RuoloRepository ruoloRepository, UtenteRepository utenteRepository)
        {
            _ruoloRepository = ruoloRepository;
            _utenteRepository = utenteRepository;
        }


        /// <summary>
        /// Restituisce il Ruolo associato all'utente mediante il suo id
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Ruolo trovato, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        public Ruolo GetRuoloById(int idRuolo)
        {
            _logger.LogInformation("Trovando il ruolo mediante il suo id: " + idRuolo);
            Ruolo ruolo = _ruoloRepository.FindById(idRuolo);
            if (ruolo == null)
            {
                _logger.LogWarning("Ruolo non trovato! Lancio una PrenotazionePrenotazioniApiException!");
                throw new PrenotazionePostazioniApiException("IdRuolo utente non trovato");
            }
            else
            {
                _logger.LogInformation("Ruolo trovato!");
                return ruolo;

            }
        }

            /// <summary>
            /// Serve a ricercare un Ruolo di un utente tramite l'id dell'utente
            /// </summary>
            /// <param name="idUtente"></param>
            /// <returns></returns>
            /// <exception cref="PrenotazionePostazioniApiException"></exception>
        public Ruolo GetRuoloByIdUtente(int idUtente)
        {
            _logger.LogInformation("Trovando un utente mediante il suo id utente: " + idUtente);
            Ruolo ruolo = _ruoloRepository.FindByIdUtente(idUtente);
            _logger.LogInformation("Controllando se l'utente e' valido...");
            if (ruolo == null)
            {
                _logger.LogWarning("Utente non trovato, ora lancio una PrenotazionePostazioniApiExcetion!");
                throw new PrenotazionePostazioniApiException("IdRuolo utente non trovato");
            }
            else
            {
                _logger.LogInformation("Utente trovato!");
                return ruolo;
            }
        }

        /// <summary>
        /// switch il ruolo dell'utente, solo se l'admin ha AccessoImpostazioni a TRUE
        /// </summary>
        /// <param name="idUtente">L'id del'utente che gli verra modificato l'accesso impostazioni</param>
        /// <param name="idAdmin">L'id dell'admin che effettua il cambiamento di accesso impostazioni all'utente</param>
        /// <returns>TRUE se l'admin ha accesso impostazioni a true, FALSE altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException">Se admin, utente, ruoloUtente, o ruoloAdmin sono null</exception>
        internal bool UpdateRuoloUtenteByAdminUtenteId(int idUtente, int idAdmin)
        {
            _logger.LogInformation("Trovando un utente mediante il suo id: " + idUtente);
            Utente utente = _utenteRepository.FindById(idUtente);
            _logger.LogInformation("Trovando un utente admin mediante il suo idL: " + idAdmin);
            Utente admin = _utenteRepository.FindById(idAdmin);
            _logger.LogInformation("Controllando se admin e' valido");
            if (admin == null){
                _logger.LogError("Admin non e' valido");
                throw new PrenotazionePostazioniApiException("admin e' null");
            }
            if (utente == null)
            {
                _logger.LogError("Utente non e' valido");
                throw new PrenotazionePostazioniApiException("utente e' null");
            }
            _logger.LogInformation("Admin e Utente validi!");
            _logger.LogInformation("Trovando il ruolo dell'utente...");
            Ruolo ruoloUtente = _ruoloRepository.FindById(utente.IdRuolo);
            _logger.LogInformation("Trovando il ruolo dell'admin...");
            Ruolo ruoloAdmin = _ruoloRepository.FindById(admin.IdRuolo);
            _logger.LogInformation("Controllando se il ruolo admin e' valido...");
            if (ruoloAdmin == null)
            {
                _logger.LogError("Il ruolo dell'admin non e' valido!");
                throw new PrenotazionePostazioniApiException("ruolo admin non trovato");
            }
            if (ruoloUtente == null)
            {
                _logger.LogError("Il ruolo dell'utente non e' valido");
                throw new PrenotazionePostazioniApiException("ruolo utente non trovato");
            }
            _logger.LogInformation("Il ruolo dell'admin e dell'utente sono validi!");
            _logger.LogInformation("Procedo con il controllo se admin ha i permessi!");
            if (!ruoloAdmin.AccessoImpostazioni)
            {
                _logger.LogError("Admin non ha i permessi!");
                return false;
            }
            _logger.LogInformation("Admin ha i permessi");
            _logger.LogInformation("Controllo se l'utente ha i permessi...");
            if (ruoloUtente.AccessoImpostazioni)
            {
                _logger.LogInformation("L'utente ha i permessi!");
                _logger.LogInformation("Chiedo di cambiare i permessi dell'utente in: " + RuoloEnum.Utente.ToString());
                _ruoloRepository.UpdateRuolo (utente.IdUtente, RuoloEnum.Utente);
            }
            else
            {
                _logger.LogInformation("L'utente NON ha i permessi!");
                _logger.LogInformation("Chiedo di cambiare i permessi dell'utente in: " + RuoloEnum.Admin.ToString());
                _ruoloRepository.UpdateRuolo(utente.IdUtente, RuoloEnum.Admin);
            }
            _logger.LogInformation("Ho cambiato il ruolo dell'utente con successo!");
            return true;

            //Ruolo utente = _ruoloRepository.FindByIdUtente(idUtente);
            //Ruolo admin = _ruoloRepository.FindByIdUtente(idAdmin);
            //if (admin == null) throw new PrenotazionePostazioniApiException("admin è null");
            //if (utente == null) throw new PrenotazionePostazioniApiException("utente è null");
            //if (admin.AccessoImpostazioni)
            //{
            //    _ruoloRepository.UpdateRuoloUtente(utente.Ruolo);
            //}
            //return true;
        }
    }
}
