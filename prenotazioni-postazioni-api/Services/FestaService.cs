using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Repositories;

namespace prenotazioni_postazioni_api.Services
{
    public class FestaService
    {
        private FestaRepository _festaRepository = new FestaRepository();
        internal List<Festa> GetByDate(DateOnly date)
        {
            return _festaRepository.FindByDate(date);
        }

        internal List<Festa> GetAll()
        {
            return _festaRepository.FindAll();
        }
    }
}
