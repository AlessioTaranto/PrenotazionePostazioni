using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class RoomService
    {
        private RoomRepository _roomRepository;
        private readonly ILog logger = LogManager.GetLogger(typeof(RoomService));
       
        public RoomService(RoomRepository roomRepository, ILogger<RoomService> logger)
        {
            _roomRepository = roomRepository;
        }



        /// <summary>
        /// restituisce tutte le stanze presenti nel database
        /// </summary>
        /// <returns>una lista di stanza</returns>
        internal List<Stanza> GetAll()
        {
            logger.Info("trovando tutte le stanze... chiamando il metodo GetAll...");
            return _roomRepository.GetAll();
        }

        /// <summary>
        /// restituisce una stanza mediante il suo id associato
        /// </summary>
        /// <param name="id">L'id della stanza</param>
        /// <returns>Stanza trovata, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal Stanza GetById(int id)
        {
            logger.Info("Troovando una stanza mediante il suo id: " + id);
            Stanza room = _roomRepository.GetById(id);
            logger.Info("Controllando se la stanza trovata e' valida...");
            if (room == null)
            {
                logger.Error("La stanza NON e' valida!");
                throw new PrenotazionePostazioniApiException("IdStanza non trovata");
            }
            logger.Info("La stanza e' valida!");
            return room;
        }

        /// <summary>
        /// restituisce una stanza mediante il suo nome associato
        /// </summary>
        /// <param name="name">il nome della stanza da trovare</param>
        /// <returns>stanza trovata, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal Stanza GetByName(string name)
        {
            logger.Info("Trovando la stanza mediante il suo nome: " + name);
            Stanza room = _roomRepository.GetByName(name);
            logger.Info("Controllando se la stanza trovata e' valida...");
            if (room == null)
            {
                logger.Error("la stanza NON e' valida...");
                throw new PrenotazionePostazioniApiException("IdStanza non trovata");
            }
            else
            {
                logger.Info("La stanza e' valida!");
                return room;
            }
        }

        /// <summary>
        /// Salva una stanza nel database
        /// </summary>
        /// <param name="roomDto">la stanza da salvare</param>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal void Add(StanzaDto roomDto)
        {
            logger.Info("Controllando se stanzaDto e' valida...");
            if (Check(roomDto))
            {
                logger.Info("stanzaDto e' valida!");
                logger.Info("Convertendo la stanzaDto in Stanza...");
                Stanza stanza = new Stanza(roomDto.Nome, roomDto.PostiMax, roomDto.PostiMaxEmergenza);
                logger.Info("Procedo con il salvataggio della stanza nel database...");
                _roomRepository.Add(stanza);
            }
            else
            {
                logger.Error("La stanza NON e' valida!");
                throw new PrenotazionePostazioniApiException("IdStanza da salvare non valida");



            }
        }

            /// <summary>
            /// Controlla se esiste già una stanza con lo stesso nome di quella che si vuole inserire
            /// </summary>
            /// <param name="roomDto"></param>
            /// <returns>True se il nome è unico, False se la stanza è già presente</returns>
            private bool Check(StanzaDto roomDto)
        {
            List<Stanza> rooms = GetAll();
            for(int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].Nome == roomDto.Nome) return false;
            }
            return true;
        }

        internal void UpdateCapacity(int capacity, int id)
        {
            if (capacity < 1)
            {
                throw new PrenotazionePostazioniApiException("Posti massimi non validi");
            }
            _roomRepository.SetCapacity(capacity, id);
            
        }

        internal void UpdateCapacityEmergency(int capacityEmergency, int id)
        {
            if (capacityEmergency < 1)
            {
                throw new PrenotazionePostazioniApiException("Posti massimi non validi");
            }
            _roomRepository.SetCapacityEmergency(capacityEmergency, id);
        }
    }
}
