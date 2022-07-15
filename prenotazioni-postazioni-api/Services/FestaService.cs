using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Repositories;
using prenotazioni_postazioni_api.Exceptions;
using prenotazione_postazioni_libs.Dto;

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

        /// <summary>
        /// salva una festaDto nel database
        /// </summary>
        /// <param name="festaDto">la festa da salvare</param>
        /// <exception cref="PrenotazionePostazioniApiException">throw nel caso in cui la data e' gia esistenze</exception>
        internal void Save(FestaDto festaDto)
        {
            if(_festaRepository.FindByDate(festaDto.Date) != null)
            {
                throw new PrenotazionePostazioniApiException("data gia occupata da un'altra festa!!!");
            }
            _festaRepository.Save(new Festa(festaDto.Date, festaDto.Desc);
        }
    }
}
