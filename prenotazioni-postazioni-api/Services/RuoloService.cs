using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;

namespace prenotazioni_postazioni_api.Services
{
    public class RuoloService
    {
        private RuoloRepository _ruoloRepository = new RuoloRepository();


        /// <summary>
        /// Restituisce il Ruolo associato all'utente mediante il suo id
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Ruolo trovato, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        public Ruolo GetRuoloById(int idRuolo)
        {
            Ruolo ruolo = _ruoloRepository.FindById(idRuolo);
            if (ruolo == null) throw new PrenotazionePostazioniApiException("Ruolo utente non trovato");
            else return ruolo;
        }

        /// <summary>
        /// Serve a ricercare un Ruolo di un utente tramite l'id dell'utente
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns></returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        //public Ruolo GetRuoloByIdUtente(int idUtente)
        //{
        //    Ruolo ruolo = _ruoloRepository.FindByIdUtente(idUtente);
        //    if (ruolo == null) throw new PrenotazionePostazioniApiException("Ruolo utente non trovato");
        //    else return ruolo;
        //}

        //public bool UpdateRuoloUtenteByAdminUtenteId(int idUtente, int idAdmin)
        //{
        //    if()

        //}
    }
}
