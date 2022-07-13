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
        internal void MakeVotoToUtente(VotoDto votoDto)
        {
            if (!votoDto.IsValid)
            {
                throw new PrenotazionePostazioniApiException("Voto non valido");
            }
            Voto voto = _votoRepository.FindByIdUtenteToAndIdUtenteFrom(votoDto.IdUtente, votoDto.IdUtenteVotato);
            if(voto == null)
            {
                _votoRepository.Save(new Voto(votoDto.IdUtente, votoDto.IdUtenteVotato, votoDto.VotoEffettuato));
                return;
            }
            if(voto.VotoEffettuato == votoDto.VotoEffettuato)
            {
                throw new PrenotazionePostazioniApiException("Il voto e' uguale");
            }
            //switch il valore del voto
            _votoRepository.UpdateVoto(voto);
        }
    }
}
