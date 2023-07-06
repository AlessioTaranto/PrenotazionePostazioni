using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Repositories;
using prenotazioni_postazioni_api.Exceptions;
using prenotazione_postazioni_libs.Dto;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class HolidayService
    {
        private HolidayRepository _holidayRepository = new HolidayRepository();
        private readonly ILog logger = LogManager.GetLogger(typeof(HolidayService));
 

        /// <summary>
        /// Restituisce tutte le feste in una data
        /// </summary>
        /// <param name="date">la data</param>
        /// <returns>lista di date</returns>
        internal Holiday GetByDate(DateTime date)
        {
            logger.Info("Chiedendo a FestaRepository di trovare una festa mediante una data...");
            return _holidayRepository.GetByDate(date);
        }


        /// <summary>
        /// Restituisce tutte le feste
        /// </summary>
        /// <returns>Lista di feste</returns>
        internal List<Holiday> GetAll()
        {
            logger.Info("Chiedendo a FestaRepository di trovare tutte le feste");
            return _holidayRepository.GetAll();
        }

        /// <summary>
        /// salva una festaDto nel database
        /// </summary>
        /// <param name="holiday">la festa da salvare</param>
        /// <exception cref="PrenotazionePostazioniApiException">throw nel caso in cui la data e' gia esistenze</exception>
        internal void Add(HolidayDto holiday)
        {
            logger.Info("Controllando se festaDto e' valida...");
            if (_holidayRepository.GetByDate(holiday.Date) != null)
            {
                logger.Warn("FestaDto non e' valida, ho lanciato una PrenotazionePostazioniApiException!");
                throw new PrenotazionePostazioniApiException("data gia occupata da un'altra festa!!!");
            }
            logger.Info("FestaDto e' valida. Cercando di salvare Festa nel database...");
            _holidayRepository.Add(new Holiday(holiday.Date, holiday.Description));
        }

        /// <summary>
        /// Rimuove una festività dal database
        /// </summary>
        /// <param name="day">Indica il giorno della festività da rimuovere</param>
        internal void Delete(DateTime day)
        {
            logger.Info("Rimozione della festa dal database...");
            _holidayRepository.Delete(day);
            logger.Info("Festività rimossa dal database");
        }
    }
}
