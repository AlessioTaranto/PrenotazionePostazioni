using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using prenotazioni_postazioni_api.Utilities;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class UserService
    {
        private UserRepository _userRepository;
        private readonly ILog logger = LogManager.GetLogger(typeof(UserService));

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }



        /// <summary>
        /// Restituisce tutti gli utenti
        /// </summary>
        /// <returns>List di Utente trovati, null altrimenti</returns>
        internal List<User> GetAll()
        {
            logger.Info("Trovando tutti gli utenti nel database...");
            return _userRepository.GetAll();
        }

        /// <summary>
        /// Resituisce l'utente mediante il suo id
        /// </summary>
        /// <param name="id">L'id dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal User GetById(int id)
        {
            logger.Info("Trovando l'utente mediante il suo id: " + id);
            User user = _userRepository.GetById(id);
            logger.Info("Controllando se l'utente trovato e' valido...");
            if (user == null)
            {
                logger.Error("L'utente trovato NON e' valido");
                throw new PrenotazionePostazioniApiException("IdUtente non trovato");
            }
            else
            {
                logger.Info("L'utente trovato e' valido");
                return user;

            }
        }

            /// <summary>
            /// Restituisce l'utente mediante la sua email
            /// </summary>
            /// <param name="email">L'email dell'utente da trovare</param>
            /// <returns>L'utente trovato, null altrimenti</returns>
            /// <exception cref="PrenotazionePostazioniApiException"></exception>
            internal User GetByEmail(string email)
        {
            logger.Info("Trovando l'utente mediante il suo email: " + email);
            User user = _userRepository.GetByEmail(email);
            logger.Info("Controllando se l'utente e' null...");
            if (user == null)
            {
                logger.Error("L'utente non e' valido");
                throw new PrenotazionePostazioniApiException("Iduser non trovato");

            }
            logger.Info("L'utente e' valido!");
            return user;
        }

        internal User GetByName(string name, string surname)
        {
            logger.Info($"Trovando l'utente mediante il suo nome: {name} {surname}");
            User user = _userRepository.GetByName(name, surname);
            logger.Info("Controllando se l'utente e' null...");
            if (user == null)
            {
                logger.Error("L'utente non e' valido");
                throw new PrenotazionePostazioniApiException("Nome utente non trovato");

            }
            logger.Info("L'utente e' valido!");
            return user;
        }


        /// <summary>
        /// Serve per salvare nel database un utente
        /// </summary>
        /// <param name="userDto"></param>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal void Add(UserDto userDto)
        {
            logger.Info("Convertendo userDto in Utente...");
            User user = new User(userDto.Name, userDto.Surname, userDto.Email, userDto.IdRole);
            logger.Info("Procedo con il salvataggio dell'utente nel database");
            _userRepository.Add(user);
        }

        /// <summary>
        /// Dato un giorno restituisce un elenco di utenti che hanno effettuato tale prenotazione quel giorno
        /// </summary>
        /// <param name="date"></param>
        /// <returns>List di Utente senza duplicati</returns>
        internal List<User>? GetByDate(DateTime date)
        {
            List<Booking> bookingsDate = _userRepository.GetAllByDate(date);
            List<User> users = new List<User>();
            foreach(Booking booking in bookingsDate)
            {
                users.Add(_userRepository.GetById(booking.IdUser));
            }
            List<User> usersWithoutDupes = users.Distinct(new UtenteEqualityComparer()).ToList();
            return usersWithoutDupes;
        }

    }
}
