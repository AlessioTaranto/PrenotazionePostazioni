
 using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Services
 {
     public class BookingService
     {
        private BookingRepository _bookingRepository;
        private RoomService _roomService;
        private SettingsService _settingsService;
        private UserService _userService;
        private readonly ILog logger = LogManager.GetLogger(typeof(BookingService));

        public BookingService (BookingRepository bookingRepository, RoomService roomService, SettingsService settingsService, UserService userService)
        {
            _bookingRepository = bookingRepository;
            _roomService = roomService;
            _settingsService = settingsService;
            _userService = userService;
        }



        /// <summary>
        /// Trova una Prenotazione dal suo ID nel Database
        /// </summary>
        /// <param name="idBooking">ID Prenotazione da trovare</param>
        /// <returns>Prenotazione trovata, altrimenti null</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        public Booking? GetById(int idBooking)
        {
            logger.Info("Cercando una prenotazione mediante il suo id " + idBooking);
            Booking? booking = _bookingRepository.GetById(idBooking);
            logger.Info("Controllando se e' una prenotazione valida...");
            if (booking == null){
                logger.Warn("Prenotazione e' null, non e' valida!");
                throw new PrenotazionePostazioniApiException("Prenotazione non trovata");
            }
            else
            {
                logger.Info("Prenotazione valida!");
                return booking;

            }
        }


         /// <summary>
         /// Trova tutte le prenotazioni dall'ID stanza nel Database
         /// </summary>
         /// <param name="idRoom">ID della stanza associata alla Prenotazione da trovare</param>
         /// <returns>Prenotazione trovata, altrimenti null</returns>
         /// <exception cref="PrenotazionePostazioniApiException"></exception>
         public List<Booking>? GetByRoom(int idRoom)
         {
            logger.Info($"Verifico che l'id {idRoom} corrisponda a una stanza valida...");
            Room room = _roomService.GetById(idRoom);

            logger.Info($"Cercando una prenotazione mediante l'id stanza {idRoom}");
            return _bookingRepository.GetByRoom(idRoom); ;
        }

        /// <summary>
        /// Trova tutte le prenotazioni fatte per una stanza in una determinata data
        /// </summary>
        /// <param name="idRoom"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        internal List<Booking>? GetAllByRoomDate(int idRoom, DateTime startDate, DateTime endDate)
        {
            logger.Info($"Cercando tutte le prenotazione di una stanza mediante una data di inizio e fine");
            logger.Info($"Id Stanza: {idRoom}");
            logger.Info("StartDate: " + startDate.ToString());
            logger.Info("EndDate: " + endDate.ToString());
            logger.Info($"Verifico che l'idStanza {idRoom} sia associato a una stanza valida");
            Room room = _roomService.GetById(idRoom);

            logger.Info("Chiamando il metodo GetAllByRoomDate()");
            return _bookingRepository.GetAllByRoomDate(idRoom, startDate, endDate);
        }
        
         /// <summary>
         /// Trova tutte le prenotazioni presenti nel Database
         /// </summary>
         /// <returns>Lista di Prenotazioni trovate nel Database</returns>
         internal List<Booking>? GetAll()
         {
             logger.Info("Chiamando il metodo GetAll() per trovare tutte le stanze");
             return _bookingRepository.GetAll();
         }

         /// <summary>
         /// Trova tutte le  Prenotazioni dall'ID dell'utente associata alla prenotazione stessa
         /// </summary>
         /// <param name="idUser">ID utente associata alla Prenotazione</param>
         /// <returns>Prenotazione trovata, altrimenti null</returns
         internal List<Booking>? GetByUser(int idUser)
         {
            logger.Info("Trovando tutte le prenotazioni di un utente, id utente: " + idUser);
            logger.Info($"Verifico che l'id {idUser} sia associato ad un utente valido");
            User utenteApp = _userService.GetById(idUser);

            logger.Info("Chiamando il metodo GetByUser()");
            return _bookingRepository.GetByUser(idUser);
         }

         /// <summary>
         /// Salva una prenotazione al database
         /// </summary>
         /// <param name="bookingDto">La prenotazione da salvare</param>
         /// <exception cref="PrenotazionePostazioniApiException">Se prenotazione e' null</exception>
         public int Add(PrenotazioneDto bookingDto)
         {
            logger.Info("Controllando se la stanza della prenotazione e' valida...");
            Room room = _roomService.GetById(bookingDto.IdStanza);
            if (room == null)
            {
                logger.Error("la stanza non e' valida!");
                throw new ArgumentException("IdRoom e' null");
            }
            logger.Info("La stanza e' valida");
            logger.Info("Controllando se siamo in stato di emergenza...");
            int roomCapacity = _settingsService.Get() ? room.CapacityEmergency : room.Capacity;
            logger.Info("Creando una nuova prenotazione...");
            Booking booking = new Booking(bookingDto.StartDate, bookingDto.EndDate, bookingDto.IdStanza, bookingDto.IdUtente);
            logger.Info("Cercando tutte le prenotazioni che sovrappongono l'orario della prenotazione che si vuole salvare...");
            List<Booking>? bookings = _bookingRepository.GetAllByRoomDate(booking.IdRoom, booking.StartDate, booking.EndDate);
            logger.Info("Controllo se prenotazioni e' null...");
            if (bookings == null)
            {
                logger.Info("Prenotazioni e' null!");
                throw new PrenotazionePostazioniApiException("Nessuna prenotazione trovata. Prenotazione e' null.");
            }
            logger.Info("Prenotazioni non e' null");
            logger.Info("Controllando se l'orario della prenotazione e' valida...");
            int timeOverlap = ControlloPrenotazioneOrePiena(booking, bookings, roomCapacity);
            if(timeOverlap == 0)
            {
                logger.Info("Prenotazione valida! Procedo con il salvataggio nel database!");
                _bookingRepository.Add(booking);
                return 0;
            }
            logger.Warn("L'orario della prenotazione non e' valida, troppe prenotazione nello stesso orario!");
            return timeOverlap;
        }

        private int ControlloPrenotazioneOrePiena(Booking newPrenotazione, List<Booking> prenotazioni, int MAX_STANZA)
        {
            
            int maxOre = 1;
            for (int i = newPrenotazione.StartDate.Hour; i <= newPrenotazione.EndDate.Hour; i++)
            {
                int contatore = 1;
                int checkContatore = 1;
                foreach (var prenotazione in prenotazioni)
                {
                    if (prenotazione.StartDate.Hour <= i && i < prenotazione.EndDate.Hour)
                    {
                        contatore++;
                    }
                    checkContatore++;
                    if (contatore > MAX_STANZA)
                    {
                        maxOre++;
                        if (contatore < checkContatore)
                        {
                            int inizioOreBlocco = contatore - maxOre - 1;
                            int fineOreBlocco = contatore;
                            return maxOre;
                        }
                    }
                }
            }
            return 0;
        }

        public void Delete(int id)
        {
            logger.Info("Cancellando la prenotazione con id [" + id + "] dal database...");
            _bookingRepository.Delete(id);
            logger.Info("La prenotazione con id [" + id + "] è stata cancellata dal database...");
        }
    }
 }
