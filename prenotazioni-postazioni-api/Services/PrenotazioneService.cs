
 using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Exceptions;

 namespace prenotazioni_postazioni_api.Services
 {
     public class PrenotazioneService
     {
        private PrenotazioneRepository _prenotazioneRepository = new PrenotazioneRepository();
        private StanzaService _stanzaService = new StanzaService();
        private ImpostazioneService _impostazioneService = new ImpostazioneService();

        /// <summary>
        /// Trova una Prenotazione dal suo ID nel Database
        /// </summary>
        /// <param name="idPrenotazione">ID Prenotazione da trovare</param>
        /// <returns>Prenotazione trovata, altrimenti null</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal Prenotazione GetPrenotazioneById(int idPrenotazione)
         {
            Prenotazione prenotazione = _prenotazioneRepository.FindById(idPrenotazione);
            if (prenotazione == null) throw new PrenotazionePostazioniApiException("Prenotazione non trovata");
            else return prenotazione;
         }


        /// <summary>
        /// Trova tutte le prenotazioni dall'ID stanza nel Database
        /// </summary>
        /// <param name="idStanza">ID della stanza associata alla Prenotazione da trovare</param>
        /// <returns>Prenotazione trovata, altrimenti null</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal List<Prenotazione> GetPrenotazioniByStanza(int idStanza)
         {
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
            return _prenotazioneRepository.FindAllByIdStanzaAndDate(idStanza, startDate, endDate);
        }
        
         /// <summary>
         /// Trova tutte le prenotazioni presenti nel Database
         /// </summary>
         /// <returns>Lista di Prenotazioni trovate nel Database</returns>
         internal List<Prenotazione> GetAllPrenotazioni()
         {
             return _prenotazioneRepository.FindAll();
         }

         /// <summary>
         /// Trova tutte le  Prenotazioni dall'ID dell'utente associata alla prenotazione stessa
         /// </summary>
         /// <param name="idUtente">ID utente associata alla Prenotazione</param>
         /// <returns>Prenotazione trovata, altrimenti null</returns
         internal List<Prenotazione> GetPrenotazioniByUtente(int idUtente)
         {
             return _prenotazioneRepository.FindByUtente(idUtente);
         }

         /// <summary>
         /// Salva una prenotazione al database
         /// </summary>
         /// <param name="prenotazioneDto">La prenotazione da salvare</param>
         public int Save(PrenotazioneDto prenotazioneDto)
         {
            Stanza stanza = _stanzaService.GetStanzaById(prenotazioneDto.IdStanza);
            if (stanza == null)
            {
                throw new ArgumentException("Stanza e' null");
            }
            int MAX_STANZA = Impostazioni.ModEmergenza ? stanza.PostiMaxEmergenza : stanza.PostiMax;
            Prenotazione newPrenotazione = new Prenotazione(prenotazioneDto.StartDate, prenotazioneDto.EndDate, prenotazioneDto.IdStanza, prenotazioneDto.IdUtente);
            List<Prenotazione> prenotazioni = _prenotazioneRepository.FindAllByIdStanzaAndDate(newPrenotazione.IdStanza,newPrenotazione.StartDate,newPrenotazione.EndDate);
            int resultOreOverlap = ControlloPrenotazioneOrePiena(newPrenotazione, prenotazioni, MAX_STANZA);
            if(resultOreOverlap == 0)
            {
                _prenotazioneRepository.Save(newPrenotazione);
                return 0;
            }
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
