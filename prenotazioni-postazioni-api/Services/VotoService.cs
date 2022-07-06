using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;

namespace prenotazioni_postazioni_api.Services
{
    public class VotoService
    {
        private VotoRepository votoRepository = new VotoRepository();

        internal Voto GetVotiFromUtente(int idUtente)
        {
            return votoRepository.FindAllByIdUtenteFrom(idUtente);
        }

        internal Voto GetVotiToUtente(int idUtente)
        {
            return votoRepository.FindAllByIdUtenteTo(idUtente);
        }

        internal Voto MakeVotoToUtente(UtenteDto utenteTo, List<UtenteDto> utenteFrom)
        {
            return votoRepository.UpdateVoti(utenteTo, utenteFrom);
        }
    }
}
