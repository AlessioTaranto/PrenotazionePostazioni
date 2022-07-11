using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
    
namespace prenotazioni_postazioni_api.Services
{
    public class VotoService
    {
        private VotoRepository _votoRepository = new VotoRepository();

        /// <summary>
        /// Serve a ottenere tutti i voti fatti da un utente
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns>Lista di voti</returns>
        internal List<Voto> GetVotiFromUtente(int idUtente)
        {
            return _votoRepository.FindAllByIdUtenteFrom(idUtente);
        }

        /// <summary>
        /// Serve a ottenere tutti i voti che sono stati fatti verso un determinato utente
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns>Lista di voti</returns>
        internal List<Voto> GetVotiToUtente(int idUtente)
        {
            return _votoRepository.FindAllByIdUtenteTo(idUtente);
        }
        
        /// <summary>
        /// Aggiunge un voto al database 
        /// </summary>
        /// <param name="votoDto"></param>
        /// <returns></returns>
        internal Voto MakeVotoToUtente(VotoDto votoDto)
        {
            int idUtenteFrom = votoDto.IdUtente;
            int idUtenteTo = votoDto.IdUtenteVotato;
            bool votoEffettuato = votoDto.VotoEffettuato;

            if (votoDto.IsValid)
            {
                Voto voto = new Voto(idUtenteFrom, idUtenteTo, votoEffettuato);
                return voto;
            }
            else throw new PrenotazionePostazioniApiException("Voto da aggiornare non valido");
        }
    }
}
