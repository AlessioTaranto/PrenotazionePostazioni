using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Repositories;

namespace prenotazioni_postazioni_api.Services
{
    public class FestaService
    {
        private FestaRepository _festaRepository = new FestaRepository();

        /// <summary>
        /// Restituisce tutte le feste in una data
        /// </summary>
        /// <param name="date">la data</param>
        /// <returns>lista di date</returns>
        internal List<Festa> GetByDate(DateOnly date)
        {
            return _festaRepository.FindByDate(date);
        }


        /// <summary>
        /// Restituisce tutte le feste
        /// </summary>
        /// <returns>Lista di feste</returns>
        internal List<Festa> GetAll()
        {
            return _festaRepository.FindAll();
        }
    }
}
