using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;

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
        public Ruolo GetRuoloByUtenteId(int idUtente)
        {
            return _ruoloRepository.FindByUtenteId(idUtente);
        }

        public bool UpdateRuoloUtenteByAdminUtenteId(UtenteDto utenteDto)
        {
            throw new NotImplementedException();
        }
    }
}
