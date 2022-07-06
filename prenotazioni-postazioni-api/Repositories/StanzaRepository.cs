using prenotazione_postazioni_libs.Models;
﻿namespace prenotazioni_postazioni_api.Repositories
{
    public class StanzaRepository
    {
        /// <summary>
        /// Query al db, restituisce tutte le stanze presente nel database
        /// </summary>
        /// <returns>Lista di Stanza</returns>
        internal List<StanzaDto> FindAll()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Query al db, restituisce una stanza mediante il suo id
        /// </summary>
        /// <param name="id">L'id della stanza</param>
        /// <returns>La stanza trovata, null altrimenti</returns>
        internal StanzaDto FindById(int id)
        {
            return null;
        }

        /// <summary>
        /// Query al db, restituisce la stanza mediante il suo nome
        /// </summary>
        /// <param name="stanzaName">Il nome della stanza da trovare</param>
        /// <returns>La stanza trovata, null altrimenti</returns>
        internal StanzaDto FindByName(string stanzaName)
        {
            return null;
        }
    }
}
