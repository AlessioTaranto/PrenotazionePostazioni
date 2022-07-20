using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using prenotazioni_postazioni_api.Utilities;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class UtenteService
    {
        private UtenteRepository _utenteRepository;
        private readonly ILog logger = LogManager.GetLogger(typeof(UtenteService));

        public UtenteService(UtenteRepository utenteRepository)
        {
            _utenteRepository = utenteRepository;
        }



        /// <summary>
        /// Restituisce tutti gli utenti
        /// </summary>
        /// <returns>List di Utente trovati, null altrimenti</returns>
        internal List<Utente> getAllUtenti()
        {
            logger.LogInformation("Trovando tutti gli utenti nel database...");
            return _utenteRepository.FindAll();
        }

        /// <summary>
        /// Resituisce l'utente mediante il suo id
        /// </summary>
        /// <param name="id">L'id dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal Utente GetUtenteById(int id)
        {
            logger.LogInformation("Trovando l'utente mediante il suo id: " + id);
            Utente utente = _utenteRepository.FindById(id);
            logger.LogInformation("Controllando se l'utente trovato e' valido...");
            if (utente == null)
            {
                logger.LogError("L'utente trovato NON e' valido");
                throw new PrenotazionePostazioniApiException("IdUtente non trovato");
            }
            else
            {
                logger.LogInformation("L'utente trovato e' valido");
                return utente;

            }
        }

            /// <summary>
            /// Restituisce l'utente mediante la sua email
            /// </summary>
            /// <param name="email">L'email dell'utente da trovare</param>
            /// <returns>L'utente trovato, null altrimenti</returns>
            /// <exception cref="PrenotazionePostazioniApiException"></exception>
            internal Utente GetUtenteByEmail(string email)
        {
            logger.LogInformation("Trovando l'utente mediante il suo email: " + email);
            Utente utente = _utenteRepository.FindByEmail(email);
            logger.LogInformation("Controllando se l'utente e' null...");
            if (utente == null)
            {
                logger.LogError("L'utente non e' valido");
                throw new PrenotazionePostazioniApiException("IdUtente non trovato");

            }
            logger.LogInformation("L'utente e' valido!");
            return utente;
        }


        /// <summary>
        /// Serve per salvare nel database un utente
        /// </summary>
        /// <param name="utenteDto"></param>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal void Save(UtenteDto utenteDto)
        {
            logger.LogInformation("Convertendo utenteDto in Utente...");
            Utente utente = new Utente(utenteDto.Nome, utenteDto.Cognome, utenteDto.Image, utenteDto.Email, utenteDto.Ruolo.IdRuolo);
            logger.LogInformation("Procedo con il salvataggio dell'utente nel database");
            _utenteRepository.Save(utente);
        }

    }
}
