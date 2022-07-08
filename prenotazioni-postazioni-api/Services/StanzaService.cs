using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;

namespace prenotazioni_postazioni_api.Services
{
    public class StanzaService
    {
        private StanzaRepository _stanzaRepository = new StanzaRepository();
        
        /// <summary>
        /// restituisce tutte le stanze presenti nel database
        /// </summary>
        /// <returns>una lista di stanza</returns>
        internal List<Stanza> GetAllStanze()
        {
            return _stanzaRepository.FindAll();
        }

        /// <summary>
        /// restituisce una stanza mediante il suo id associato
        /// </summary>
        /// <param name="id">L'id della stanza</param>
        /// <returns>Stanza trovata, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal Stanza GetStanzaById(int id)
        {
            Stanza stanza = _stanzaRepository.FindById(id);
            if (stanza == null) throw new PrenotazionePostazioniApiException("Stanza non trovata");
            else return stanza;
        }

        /// <summary>
        /// restituisce una stanza mediante il suo nome associato
        /// </summary>
        /// <param name="stanzaName">il nome della stanza da trovare</param>
        /// <returns>stanza trovata, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal Stanza GetStanzaByName(string stanzaName)
        {
            Stanza stanza = _stanzaRepository.FindByName(stanzaName);
            if (stanza == null) throw new PrenotazionePostazioniApiException("Stanza non trovata");
            else
                return stanza;
        }

        /// <summary>
        /// Salva una stanza nel database
        /// </summary>
        /// <param name="stanzaDto">la stanza da salvare</param>
        internal void Save(StanzaDto stanzaDto)
        {
            string nomestanza = stanzaDto.Nome;
            int postiMax = stanzaDto.PostiMax;
            int postiMaxEmergenza = stanzaDto.PostiMaxEmergenza;

            if (stanzaDto.IsValid && CheckStanza(stanzaDto))
            {
                Stanza stanza = new Stanza(nomestanza, postiMax, postiMaxEmergenza); //TODO aggiungere costruttore
                _stanzaRepository.Save(stanza);
            }
            else throw new PrenotazionePostazioniApiException("Stanza da salvare non valida");


        }

        private bool CheckStanza(StanzaDto stanzaDto)
        {
            List<Stanza> stanze = GetAllStanze();
            for(int i = 0; i < stanze.Count; i++)
            {
                if (stanze[i].Nome == stanzaDto.Nome) return false;
            }
            return true;
        }
    }
}
