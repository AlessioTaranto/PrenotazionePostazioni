using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;

namespace prenotazioni_postazioni_api.Services
{
    public class RuoloService
    {
        private RuoloRepository _ruoloRepository = new RuoloRepository();
        private UtenteRepository _utenteRepository = new UtenteRepository();


        /// <summary>
        /// Restituisce il Ruolo associato all'utente mediante il suo id
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Ruolo trovato, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        public Ruolo GetRuoloById(int idRuolo)
        {
            Ruolo ruolo = _ruoloRepository.FindById(idRuolo);
            if (ruolo == null) throw new PrenotazionePostazioniApiException("IdRuolo utente non trovato");
            else return ruolo;
        }

        /// <summary>
        /// Serve a ricercare un Ruolo di un utente tramite l'id dell'utente
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns></returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        public Ruolo GetRuoloByIdUtente(int idUtente)
        {
            Ruolo ruolo = _ruoloRepository.FindByIdUtente(idUtente);
            if (ruolo == null) throw new PrenotazionePostazioniApiException("IdRuolo utente non trovato");
            else
            {
                return ruolo;
            }
        }

        /// <summary>
        /// switch il ruolo dell'utente, solo se l'admin ha AccessoImpostazioni a TRUE
        /// </summary>
        /// <param name="idUtente">L'id del'utente che gli verra modificato l'accesso impostazioni</param>
        /// <param name="idAdmin">L'id dell'admin che effettua il cambiamento di accesso impostazioni all'utente</param>
        /// <returns>TRUE se l'admin ha accesso impostazioni a true, FALSE altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException">Se admin, utente, ruoloUtente, o ruoloAdmin sono null</exception>
        internal bool UpdateRuoloUtenteByAdminUtenteId(int idUtente, int idAdmin)
        {
            Utente utente = _utenteRepository.FindById(idUtente);
            Utente admin = _utenteRepository.FindById(idAdmin);
            if (admin == null) throw new PrenotazionePostazioniApiException("admin e' null");
            if (utente == null) throw new PrenotazionePostazioniApiException("utente e' null");
            Ruolo ruoloUtente = _ruoloRepository.FindById(utente.IdRuolo.IdRuolo);
            Ruolo ruoloAdmin = _ruoloRepository.FindById(admin.IdRuolo.IdRuolo);
            if (ruoloAdmin == null) throw new PrenotazionePostazioniApiException("ruolo admin non trovato");
            if (ruoloUtente == null) throw new PrenotazionePostazioniApiException("ruolo utente non trovato");

            if (!ruoloAdmin.AccessoImpostazioni)
            {
                return false;
            }
            if (ruoloUtente.AccessoImpostazioni)
            {
                _ruoloRepository.UpdateRuolo (utente.IdUtente, RuoloEnum.Utente);
            }
            else
            {
                _ruoloRepository.UpdateRuolo(utente.IdUtente, RuoloEnum.Admin);
            }
            return true;

            //Ruolo utente = _ruoloRepository.FindByIdUtente(idUtente);
            //Ruolo admin = _ruoloRepository.FindByIdUtente(idAdmin);
            //if (admin == null) throw new PrenotazionePostazioniApiException("admin è null");
            //if (utente == null) throw new PrenotazionePostazioniApiException("utente è null");
            //if (admin.AccessoImpostazioni)
            //{
            //    _ruoloRepository.UpdateRuoloUtente(utente.Ruolo);
            //}
            //return true;
        }
    }
}
