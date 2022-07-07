using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;

namespace prenotazioni_postazioni_api.Services
{
    public class VotoService
    {
        private VotoRepository _votoRepository = new VotoRepository();

        internal List<Voto> GetVotiFromUtente(int idUtente)
        {
            return _votoRepository.FindAllByIdUtenteFrom(idUtente);
        }

        internal List<Voto> GetVotiToUtente(int idUtente)
        {
            return _votoRepository.FindAllByIdUtenteTo(idUtente);
        }

        internal Voto MakeVotoToUtente(UtenteDto utenteTo, List<UtenteDto> utenteFrom)
        {
            return null;
            //return votoRepository.UpdateVoti(utenteTo, utenteFrom);
        }
    }
}
