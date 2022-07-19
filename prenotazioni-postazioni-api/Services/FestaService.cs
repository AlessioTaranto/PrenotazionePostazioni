using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Repositories;
using prenotazioni_postazioni_api.Exceptions;
using prenotazione_postazioni_libs.Dto;

namespace prenotazioni_postazioni_api.Services
{
    public class FestaService
    {
        private FestaRepository _festaRepository = new FestaRepository();
        private readonly ILogger<FestaService> logger;

        public FestaService(ILogger<FestaService> logger)
        {
            this.logger = logger;
        }

        public FestaService() { }

        /// <summary>
        /// Restituisce tutte le feste in una data
        /// </summary>
        /// <param name="date">la data</param>
        /// <returns>lista di date</returns>
        internal Festa GetByDate(DateTime date)
        {
            logger.LogInformation("Chiedendo a FestaRepository di trovare una festa mediante una data...");
            return _festaRepository.FindByDate(date);
        }


        /// <summary>
        /// Restituisce tutte le feste
        /// </summary>
        /// <returns>Lista di feste</returns>
        internal List<Festa> GetAll()
        {
            logger.LogInformation("Chiedendo a FestaRepository di trovare tutte le feste");
            return _festaRepository.FindAll();
        }

        /// <summary>
        /// salva una festaDto nel database
        /// </summary>
        /// <param name="festaDto">la festa da salvare</param>
        /// <exception cref="PrenotazionePostazioniApiException">throw nel caso in cui la data e' gia esistenze</exception>
        internal void Save(FestaDto festaDto)
        {
            logger.LogInformation("Controllando se festaDto e' valida...");
            if(_festaRepository.FindByDate(festaDto.Date) != null)
            {
                logger.LogWarning("FestaDto non e' valida, ho lanciato una PrenotazionePostazioniApiException!");
                throw new PrenotazionePostazioniApiException("data gia occupata da un'altra festa!!!");
            }
            logger.LogWarning("FestaDto e' valida. Cercando di salvare Festa nel database...");
            _festaRepository.Save(new Festa(festaDto.Date, festaDto.Desc));
        }
    }
}
