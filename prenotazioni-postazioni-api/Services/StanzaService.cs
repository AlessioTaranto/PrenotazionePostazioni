
﻿using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;

namespace prenotazioni_postazioni_api.Services
{
    public class StanzaService
    {
        private StanzaRepository stanzaRepository = new StanzaRepository();
        
        /// <summary>
        /// restituisce tutte le stanze presenti nel database
        /// </summary>
        /// <returns>una lista di stanza</returns>
        internal List<StanzaDto> GetAllStanze()
        {
            return stanzaRepository.FindAll();
        }

        /// <summary>
        /// restituisce una stanza mediante il suo id associato
        /// </summary>
        /// <param name="id">L'id della stanza</param>
        /// <returns>Stanza trovata, null altrimenti</returns>
        internal StanzaDto GetStanzaByid(int id)
        {
            return stanzaRepository.FindById(id);
        }

        /// <summary>
        /// restituisce una stanza mediante il suo nome associato
        /// </summary>
        /// <param name="stanzaName">il nome della stanza da trovare</param>
        /// <returns>stanza trovata, null altrimenti</returns>
        internal StanzaDto GetStanzaByName(string stanzaName)
        {
            return stanzaRepository.FindByName(stanzaName);
        }

        /// <summary>
        /// Salva una stanza nel database
        /// </summary>
        /// <param name="stanzaDto">la stanza da salvare</param>
        internal void Save(StanzaDto stanzaDto)
        {
            //TODO da implemenetare
        }
    }
}
