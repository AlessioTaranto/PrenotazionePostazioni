
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
        internal List<Prenotazione> GetAllPrenotazioniByIdStanzaAndDate(int idStanza, DateTime dateDay)
        {
            return _prenotazioneRepository.FindAllByIdStanzaAndDate(idStanza, dateDay);
        }
        \
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
         public void Save(PrenotazioneDto prenotazioneDto)
         {
            int idStanza = prenotazioneDto.IdStanza;
            int idUtente = prenotazioneDto.IdUtente;
            Stanza stanza = _stanzaService.GetStanzaById(idStanza);
            DateTime startDate = prenotazioneDto.StartDate;
            DateTime endDate = prenotazioneDto.EndDate;
            int postiMax = _impostazioneService.GetImpostazioneEmergenza() == false ? stanza.PostiMax : stanza.PostiMaxEmergenza;
            List<Prenotazione> prenotazioni = FindAllByIdStanzaAndDate(idStanza, date);

            if (prenotazioni.Count < postiMax)
            {
                Prenotazione prenotazione = new Prenotazione(date, idStanza, idUtente);
                _prenotazioneRepository.Save(prenotazione);
            }
            else throw new PrenotazionePostazioniApiException("Posti liberi esauriti");

         }


     }
 }
