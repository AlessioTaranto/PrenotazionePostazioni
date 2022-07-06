<<<<<<< HEAD
﻿using prenotazioni_postazioni_api.Repositories;

namespace prenotazioni_postazioni_api.Services
{
    public class PrenotazioneService
    {
        private PrenotazioneRepository prenotazioneRepository = new PrenotazioneRepository();
        /// <summary>
        /// Trova una Prenotazione dal suo ID nel Database
        /// </summary>
        /// <param name="idPrenotazione">ID Prenotazione da trovare</param>
        /// <returns>Prenotazione trovata, altrimenti null</returns>
        internal Prenotazione GetPrenotazioneById(int idPrenotazione)
        {
            return prenotazioneRepository.FindById(idPrenotazione);
        }


        /// <summary>
        /// Trova una Prenotazione dall'ID stanza nel Database
        /// </summary>
        /// <param name="idStanza">ID della stanza associata alla Prenotazione da trovare</param>
        /// <returns>Prenotazione trovata, altrimenti null</returns>
        internal Prenotazione GetPrenotazioneByStanza(string idStanza)
        {
            return prenotazioneRepository.FindByStanza(idStanza);
        }
        /// <summary>
        /// Trova tutte le prenotazioni presenti nel Database
        /// </summary>
        /// <returns>Lista di Prenotazioni trovate nel Database</returns>
        internal List<Prenotazione> GetAllPrenotazioni()
        {
            return prenotazioneRepository.FindAll();
        }

        /// <summary>
        /// Trova una Prenotazione dall'ID dell'utente associata alla prenotazione stessa
        /// </summary>
        /// <param name="idUtente">ID utente associata alla Prenotazione</param>
        /// <returns>Prenotazione trovata, altrimenti null</returns
        internal Prenotazione GetPrenotazioneByUtente(string idUtente)
        {
            return prenotazioneRepository.FindByUtente(idUtente);
        }

        /// <summary>
        /// Salva una prenotazione al database
        /// </summary>
        /// <param name="prenotazioneDto">La prenotazione da salvare</param>
        public void Save(PrenotazioneDto prenotazioneDto)
        {
            //salva una prenotazione nel database
        }
    }
=======
﻿using prenotazioni_postazioni_api.Repositories;

namespace prenotazioni_postazioni_api.Services
{
    public class PrenotazioneService
    {
        private PrenotazioneRepository prenotazioneRepository = new PrenotazioneRepository();
        /// <summary>
        /// Trova una Prenotazione dal suo ID nel Database
        /// </summary>
        /// <param name="idPrenotazione">ID Prenotazione da trovare</param>
        /// <returns>Prenotazione trovata, altrimenti null</returns>
        internal Prenotazione GetPrenotazioneById(int idPrenotazione)
        {
            return prenotazioneRepository.FindById(idPrenotazione);
        }


        /// <summary>
        /// Trova una Prenotazione dall'ID stanza nel Database
        /// </summary>
        /// <param name="idStanza">ID della stanza associata alla Prenotazione da trovare</param>
        /// <returns>Prenotazione trovata, altrimenti null</returns>
        internal Prenotazione GetPrenotazioneByStanza(string idStanza)
        {
            return prenotazioneRepository.FindByStanza(idStanza);
        }
        /// <summary>
        /// Trova tutte le prenotazioni presenti nel Database
        /// </summary>
        /// <returns>Lista di Prenotazioni trovate nel Database</returns>
        internal List<Prenotazione> GetAllPrenotazioni()
        {
            return prenotazioneRepository.FindAll();
        }

        /// <summary>
        /// Trova una Prenotazione dall'ID dell'utente associata alla prenotazione stessa
        /// </summary>
        /// <param name="idUtente">ID utente associata alla Prenotazione</param>
        /// <returns>Prenotazione trovata, altrimenti null</returns
        internal Prenotazione GetPrenotazioneByUtente(string idUtente)
        {
            return prenotazioneRepository.FindByUtente(idUtente);
        }

        /// <summary>
        /// Salva una prenotazione al database
        /// </summary>
        /// <param name="prenotazioneDto">La prenotazione da salvare</param>
        public void Save(PrenotazioneDto prenotazioneDto)
        {
            //salva una prenotazione nel database
        }
    }
>>>>>>> b0c92be8d75a2fa54833ed57e5e1ab6908317d6e
}