
 using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Exceptions;

 namespace prenotazioni_postazioni_api.Services
 {
     public class PrenotazioneService
     {
        private PrenotazioneRepository _prenotazioneRepository;
        private StanzaService _stanzaService;
        private ImpostazioneService _impostazioneService;
        private readonly ILogger<PrenotazioneService> logger;

        public PrenotazioneService (PrenotazioneRepository prenotazioneRepository, StanzaService stanzaService, ImpostazioneService impostazioneService, ILogger<PrenotazioneService> logger)
        {
            _prenotazioneRepository = prenotazioneRepository;
            _stanzaService = stanzaService;
            _impostazioneService = impostazioneService;
            this.logger = logger;
        }



        /// <summary>
        /// Trova una Prenotazione dal suo ID nel Database
        /// </summary>
        /// <param name="idPrenotazione">ID Prenotazione da trovare</param>
        /// <returns>Prenotazione trovata, altrimenti null</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        public Prenotazione GetPrenotazioneById(int idPrenotazione)
        {
            logger.LogInformation("Cercando una prenotazione mediante il suo id " + idPrenotazione);
            Prenotazione prenotazione = _prenotazioneRepository.FindById(idPrenotazione);
            logger.LogInformation("Controllando se e' una prenotazione valida...");
            if (prenotazione == null{
                logger.LogWarning("Prenotazione e' null, non e' valida!");
                throw new PrenotazionePostazioniApiException("Prenotazione non trovata");
            }
            else
            {
                logger.LogInformation("Prenotazione valida!");
                return prenotazione;

            }
        }


            /// <summary>
            /// Trova tutte le prenotazioni dall'ID stanza nel Database
            /// </summary>
            /// <param name="idStanza">ID della stanza associata alla Prenotazione da trovare</param>
            /// <returns>Prenotazione trovata, altrimenti null</returns>
            /// <exception cref="PrenotazionePostazioniApiException"></exception>
            public List<Prenotazione> GetPrenotazioniByStanza(int idStanza)
         {
            logger.LogInformation($"Cercando una prenotazione mediante l'id stanza {idStanza}");
            return _prenotazioneRepository.FindByStanza(idStanza); ;
        }

        /// <summary>
        /// Trova tutte le prenotazioni fatte per una stanza in una determinata data
        /// </summary>
        /// <param name="idStanza"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        internal List<Prenotazione> GetAllPrenotazioniByIdStanzaAndDate(int idStanza, DateTime startDate, DateTime endDate)
        {
            logger.LogInformation($"Cercando tutte le prenotazione di una stanza mediante una data di inizio e fine");
            logger.LogInformation($"Id Stanza: {idStanza}");
            logger.LogInformation("StartDate: " + startDate.ToString());
            logger.LogInformation("EndDate: " + endDate.ToString());
            logger.LogInformation("Chiamando il metodo FindAllByIdStanzaAndDate()");
            return _prenotazioneRepository.FindAllByIdStanzaAndDate(idStanza, startDate, endDate);
        }
        
         /// <summary>
         /// Trova tutte le prenotazioni presenti nel Database
         /// </summary>
         /// <returns>Lista di Prenotazioni trovate nel Database</returns>
         internal List<Prenotazione> GetAllPrenotazioni()
         {
            logger.LogInformation("Chiamando il metodo FindAll per trovare tutte le stanze");
             return _prenotazioneRepository.FindAll();
         }

         /// <summary>
         /// Trova tutte le  Prenotazioni dall'ID dell'utente associata alla prenotazione stessa
         /// </summary>
         /// <param name="idUtente">ID utente associata alla Prenotazione</param>
         /// <returns>Prenotazione trovata, altrimenti null</returns
         internal List<Prenotazione> GetPrenotazioniByUtente(int idUtente)
        {
            logger.LogInformation("Trovando tutte le prenotazioni di un utente, id utente: " + idUtente);
            logger.LogInformation("Chiamando il metodo FindByUtente()");
            return _prenotazioneRepository.FindByUtente(idUtente);
         }

         /// <summary>
         /// Salva una prenotazione al database
         /// </summary>
         /// <param name="prenotazioneDto">La prenotazione da salvare</param>
         public int Save(PrenotazioneDto prenotazioneDto)
         {
            logger.LogInformation("Controllando se la stanza della prenotazione e' valida...");
            Stanza stanza = _stanzaService.GetStanzaById(prenotazioneDto.Stanza.IdStanza);
            if (stanza == null)
            {
                logger.LogError("la stanza non e' valida!");
                throw new ArgumentException("IdStanza e' null");
            }
            logger.LogInformation("La stanza e' valida");
            logger.LogInformation("Controllando se siamo in stato di emergenza...");
            int MAX_STANZA = _impostazioneService.GetImpostazioneEmergenza() ? stanza.PostiMaxEmergenza : stanza.PostiMax;
            logger.LogInformation("Creando una nuova prenotazione...");
            Prenotazione newPrenotazione = new Prenotazione(prenotazioneDto.StartDate, prenotazioneDto.EndDate, prenotazioneDto.Stanza.IdStanza, prenotazioneDto.Utente.IdUtente);
            logger.LogInformation("Cercando tutte le prenotazioni che sovrappongono l'orario della prenotazione che si vuole salvare...");
            List<Prenotazione> prenotazioni = _prenotazioneRepository.FindAllByIdStanzaAndDate(newPrenotazione.IdStanza, newPrenotazione.StartDate, newPrenotazione.EndDate);
            logger.LogInformation("Controllando se l'orario della prenotazione e' valida...");
            int resultOreOverlap = ControlloPrenotazioneOrePiena(newPrenotazione, prenotazioni, MAX_STANZA);
            if(resultOreOverlap == 0)
            {
                logger.LogInformation("Prenotazione valida! Procedo con il salvataggio nel database!");
                _prenotazioneRepository.Save(newPrenotazione);
                return 0;
            }
            logger.LogWarning("L'orario della prenotazione non e' valida, troppe prenotazione nello stesso orario!");
            return resultOreOverlap;
        }

        private int ControlloPrenotazioneOrePiena(Prenotazione newPrenotazione, List<Prenotazione> prenotazioni, int MAX_STANZA)
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
    }
 }
