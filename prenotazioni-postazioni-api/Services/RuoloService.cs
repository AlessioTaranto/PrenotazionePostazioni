<<<<<<< HEAD
﻿using prenotazioni_postazioni_api.Repositories;

namespace prenotazioni_postazioni_api.Services
{
    public class RuoloService
    {
        private RuoloRepository ruoloRepository = new RuoloRepository();


        /// <summary>
        /// Restituisce il Ruolo associato all'utente mediante il suo id
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Ruolo trovato, null altrimenti</returns>
        public Ruolo GetRuoloByUtenteId(int idUtente)
        {
            return ruoloRepository.FindByUtenteId(idUtente);
        }
    }
}
=======
﻿using prenotazioni_postazioni_api.Repositories;

namespace prenotazioni_postazioni_api.Services
{
    public class RuoloService
    {
        private RuoloRepository ruoloRepository = new RuoloRepository();


        /// <summary>
        /// Restituisce il Ruolo associato all'utente mediante il suo id
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Ruolo trovato, null altrimenti</returns>
        public Ruolo GetRuoloByUtenteId(int idUtente)
        {
            return ruoloRepository.FindByUtenteId(idUtente);
        }
    }
}
>>>>>>> b0c92be8d75a2fa54833ed57e5e1ab6908317d6e
